using System.ComponentModel.DataAnnotations;

namespace AngularProject.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Src { get; set; }
    }
}
