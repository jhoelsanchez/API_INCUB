using System.Text.Json.Serialization;

namespace InClub.Core.Models
{
    public class OrderDetail
    {
        [JsonIgnore]
        public int OrderId { get; set; }
        [JsonIgnore]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public Product Product { get; set; }

        public OrderDetail()
        {
            Product = new Product();
        }

        public OrderDetail(Product product, int orderId, int productId, int quantity, decimal price, decimal discount)
        {
            Product = product;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
            Discount = discount;
        }
    }
}