using AngularProject.Models;
using DotNetWebAPI.DTOs;

namespace AngularAPI.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Title_EN { get; set; }
        public string Title_AR { get; set; }
        public string Details_EN { get; set; }
        public string Details_AR { get; set; }
        public decimal price { get; set; }

        public string Color { get; set; }
        public int Quantity { get; set; }
        public ICollection<string>? Images { get; set; }
        //public string? Category { get; set; }
        public ProductCategoryDto? Category { get; set; }
        public int? CategoryID { get; set; }

        public int Rating { get; set; }
        public OwnerDto? Owner { get; set; }

    }
}
