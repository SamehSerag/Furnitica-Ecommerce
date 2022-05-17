using System.ComponentModel.DataAnnotations;

namespace DotNetWebAPI.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string ReviewBody { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int starsCount { get; set; }

        public int ProductId { get; set; }
    }
}
