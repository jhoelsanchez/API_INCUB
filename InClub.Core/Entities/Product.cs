namespace InClub.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}