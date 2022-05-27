using AngularProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetWebAPI.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? user { get; set; }

        public string ReviewBody { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int starsCount { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
