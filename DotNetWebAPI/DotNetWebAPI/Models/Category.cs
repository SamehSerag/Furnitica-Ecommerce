using System.ComponentModel.DataAnnotations;

namespace AngularProject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }

        public string? Image { get; set; }

        public ICollection<Product>? Products { get; set; }
    
    }
}
