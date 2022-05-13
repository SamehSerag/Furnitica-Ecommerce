using System.ComponentModel.DataAnnotations;

namespace AngularProject.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
