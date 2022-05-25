using AngularProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetWebAPI.Models
{
    public class WishListProduct
    {
        [ForeignKey("wishList")]
        public int WishListId { get; set; }
        WishList wishList { get; set; }

        [ForeignKey("product")]
        public int ProductId { get; set; }

        public Product product { get; set; }
    }
}
