using AngularAPI.Enums;
using AngularProject.Models;
using DotNetWebAPI.DTOs;

namespace AngularAPI.Dtos
{
    public class AdminProductDto
    {
       
        public int Id { get; set; }
        public string Title_EN { get; set; }
        public string Title_AR { get; set; }
        public string Details_EN { get; set; }
        public string Details_AR { get; set; }
        public decimal price { get; set; }

        public Color Color { get; set; }
        public int Quantity { get; set; }

        public int CategoryID { get; set; }
        public ProductCategoryDto? Category { get; set; }
        public ICollection<ImageDto>? Image { get; set; }
        public ICollection<OrderProductDto>? OrderProduct { get; set; } 


    }
}
