using System.ComponentModel.DataAnnotations;

namespace DotNetWebAPI.DTOs
{
    public class RoleDto
    {
        [Required]
        public string? RoleName { get; set; }
    }
}
