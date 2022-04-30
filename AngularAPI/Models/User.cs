using AngularProject.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50), Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required]
        public string Address { get; set; }

        [Required, ForeignKey("Role")]
        public int RoleId { get; set; }

        [Required]
        public int CartID { get; set; }

        [ForeignKey("Image")]
        public int ImageId { get; set; }
        public Image? Image { get; set; }

        public Role? Role { get; set; }

        //// list of orders
        public ICollection<Order> Orders { get; set; }


    }
}
