using AngularProject.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularProject.Models
{
    public class Order
    {
       [Key]
       public int Id { get; set; }
       [Required, DataType(DataType.Date)]
       public DateTime Date { get; set; }

       [Required, DataType(DataType.Currency)]
       public decimal TotalPrice { get; set; }
       
       [Required, EnumDataType(typeof(OrderState))]
       public OrderState State { get; set; }

       [Required, ForeignKey("User")]
       public int UserID;
       public User User { get; set; }

       public ICollection<OrderProducts> OrderProducts { get; set; }

    }
}
