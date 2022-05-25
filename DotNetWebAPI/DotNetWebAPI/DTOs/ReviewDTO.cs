using System.ComponentModel.DataAnnotations;

namespace DotNetWebAPI.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserImg { get; set; }

        public string ReviewBody { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int starsCount { get; set; }

        public int ProductId { get; set; }
    }
}
