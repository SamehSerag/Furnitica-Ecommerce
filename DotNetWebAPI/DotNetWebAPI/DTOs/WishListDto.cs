namespace DotNetWebAPI.DTOs
{
    public class WishListDto
    {
        public int productId { get; set; }

        public string title_EN { get; set; }
        public string title_AR { get; set; }
        public decimal price { get; set; }
        public string availability { get; set; }
        public string Image { get; set; }

    }
}
