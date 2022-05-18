using AngularProject.Enums;
using System.ComponentModel.DataAnnotations;

namespace AngularAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string? username { get; set; }

        [Required]
        public string? password { get; set; }


        [Required]
        public string? email { get; set; }


        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        //[Required]
        public string? Address { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
