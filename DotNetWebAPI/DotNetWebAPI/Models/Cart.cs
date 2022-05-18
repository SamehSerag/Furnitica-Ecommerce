using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AngularProject.Models
{
    //customerBasket
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("User")]
        public string UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

        public List<CartProduct> CartProducts { get; set; } = new List<CartProduct>();
    }
}
