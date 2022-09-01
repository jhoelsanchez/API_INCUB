using Dapper;
using InClub.Application.Interfaces;
using InClub.Core.Models;
using InClub.Infraestructure.Context;

namespace InClub.Infraestructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly RepositoryContext context;

        public ProductRepository(RepositoryContext context)
        {
            this.context = context;
        }

        public async Task<int> AddAsync(Product entity)
        {
            entity.Created = DateTime.Now;
            var sql = "Insert into Products (Name, Description, Price, Created, Status) VALUES (@Name, @Description, @Price, @Created, @Status)";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.QueryAsync<Product>(sql);
                return result.ToList();
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            entity.Updated = DateTime.Now;
            var sql = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, Updated = @Updated  WHERE Id = @Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}