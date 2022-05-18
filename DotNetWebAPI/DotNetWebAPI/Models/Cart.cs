using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AngularProject.Models
{
    //customerBasket
    public class Cart
    {
        [Key]
        public string CartId { get; set; }
        public string UserId { get; set; }
    }
}
