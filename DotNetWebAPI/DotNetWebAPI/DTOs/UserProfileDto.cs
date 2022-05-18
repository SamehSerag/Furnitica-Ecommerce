using AngularProject.Enums;
using AngularProject.Models;

namespace DotNetWebAPI.DTOs
{
    public class UserProfileDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public int ImageId { get; set; }
    }
}
