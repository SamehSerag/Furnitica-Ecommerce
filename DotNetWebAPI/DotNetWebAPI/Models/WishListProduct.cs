using AngularProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetWebAPI.Models
{
    public class WishListProduct
    {
        [ForeignKey("user")]
        public string UserId { get; set; }

        public User user { get; set; }


        [ForeignKey("product")]
        public int ProductId { get; set; }

        public Product product { get; set; }
    }
}
