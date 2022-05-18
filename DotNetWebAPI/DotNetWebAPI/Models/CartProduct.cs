using AngularAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularProject.Models
{
    public class CartProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title_EN { get; set; }
        public string Title_AR { get; set; }

        [DataType(DataType.Currency), Required]
        public decimal price { get; set; }
        public int Quantity { get; set; }
        public Category? Category { get; set; }

    }
}
