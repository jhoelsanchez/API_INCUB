using Dapper;
using InClub.Application.Interfaces;
using InClub.Core.Models;
using InClub.Infraestructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InClub.Infraestructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RepositoryContext context;

        public OrderRepository(RepositoryContext context)
        {
            this.context = context;
        }

        public async Task<int> AddAsync(Order entity)
        {
            int orderId = 0;
            entity.Created = DateTime.Now;
            var sql = "INSERT INTO Orders(CustomerId, Notes, Created, Status) VALUES (@CustomerId, @Notes, @Created, @Status); SELECT CAST(SCOPE_IDENTITY() as int)";

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            orderDetails = entity.Details.ToList();

            using (var connection = context.CreateConnection())
            {
                orderId = await connection.QuerySingleAsync<int>(sql, entity);
                if (orderId != 0)
                {
                    await InsertUpdateOrder(orderDetails, orderId);
                }
            }

            return orderId;
        }

        public async Task<int> InsertUpdateOrder(List<OrderDetail> orderDetails, int orderId)
        {
            int result = 0;
            if (orderId != 0)
            {
                using (var connection = context.CreateConnection())
                {
                    foreach (OrderDetail item in orderDetails)
                    {
                        item.OrderId = orderId;
                        item.Total = (item.Price * item.Quantity) - item.Discount;
                        var qry = @"INSERT INTO OrderDetails(OrderId, ProductId, Price, Quantity, Discount, Total) VALUES (@OrderId, @ProductId, @Price, @Quantity, @Discount, @Total)";

                        result = await connection.ExecuteAsync(qry, item);
                    }
                }
            }
            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Orders WHERE Id = @Id DELETE FROM OrderDetails WHERE OrderId = @Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync()
        {
            List<Order> orders = new List<Order>();
            var sql = "SELECT * FROM Orders";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.QueryAsync<Order>(sql);
                orders = result.ToList();
                foreach (var order in orders)
                {
                    var res = await connection.QueryAsync<OrderDetail>(@"SELECT * FROM OrderDetails WHERE OrderId = @Id", new { Id = order.Id });
                    order.Details = res.ToList();
                    foreach (var item in order.Details)
                    {
                        var prd = await connection.QueryAsync<Product>(@"SELECT * FROM Products WHERE Id = @ProductId", new { ProductId = item.ProductId });
                        item.Product = prd.First();
                    }                                     
                }

                return orders;
            }
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            Order order = new Order();
            var sql = "SELECT * FROM Orders WHERE Id = @Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.QueryAsync<Order>(sql, new { Id = id });
                order = result.FirstOrDefault();

                if (order != null)
                {
                    var res = await connection.QueryAsync<OrderDetail>(@"SELECT * FROM OrderDetails WHERE OrderId = @Id", new { Id = order.Id });
                    order.Details = res.ToList();
                    foreach (var item in order.Details)
                    {
                        var prd = await connection.QueryAsync<Product>(@"SELECT * FROM Products WHERE Id = @ProductId", new { ProductId = item.ProductId });
                        item.Product = prd.First();
                    }
                }

                return order;
            }
        }

        public async Task<int> UpdateAsync(Order entity)
        {
            entity.Updated = DateTime.Now;
            var sql = "UPDATE Orders CustomerId = @CustomerId, Notes = @Notes, Updated = @Updated, Status = @Status WHERE Id = @Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}