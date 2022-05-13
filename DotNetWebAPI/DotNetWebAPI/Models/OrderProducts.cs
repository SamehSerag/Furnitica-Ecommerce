using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularProject.Models
{
    public class OrderProducts
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public int Quantity { get; set; }

        public Order? Order { get; set; }
        public Product? Product { get; set; }

    }
}
