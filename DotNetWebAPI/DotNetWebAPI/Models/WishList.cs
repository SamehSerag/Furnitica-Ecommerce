using AngularProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetWebAPI.Models
{
    public class WishList
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User user { get; set; }

        public List<Product> Products { get; set; }
    }
}
