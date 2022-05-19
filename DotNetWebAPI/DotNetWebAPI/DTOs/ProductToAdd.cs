namespace DotNetWebAPI.DTOs
{
    public class ProductToAdd
    {
        public string Title_EN { get; set; }
        public string Title_AR { get; set; }
        public string Details_EN { get; set; }
        public string Details_AR { get; set; }
        public decimal price { get; set; }

        public int Color { get; set; }
        public int Quantity { get; set; }
        public int? CategoryID { get; set; }
        public string? OwnerId { get; set; }
    }
}
