using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Xml.Schema;

namespace InClub.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int CustomerId { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Status { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetail> Details { get; set; }

        public Order()
        {
            Customer = new Customer();
            Details = new List<OrderDetail>();
        }

        public Order(int id, int customerId, DateTime created, DateTime updated, int status, Customer customer, List<OrderDetail> details)
        {
            Id = id;
            CustomerId = customerId;
            Created = created;
            Updated = updated;
            Status = status;
            Customer = customer;
            Details = details;
        }
    }
}