using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularProject.Models
{
    public class CartProduct
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Cart")]
        public int CartId { get; set; }
        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }

        public Product? Product { get; set; }
        public Cart? Cart { get; set; }
    }
}
