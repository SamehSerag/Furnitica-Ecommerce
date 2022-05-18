using AngularProject.Models;

namespace DotNetWebAPI.DTOs
{
    public class CartProductDto
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
