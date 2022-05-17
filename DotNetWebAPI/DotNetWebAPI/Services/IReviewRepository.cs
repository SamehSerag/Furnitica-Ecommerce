using DotNetWebAPI.Models;

namespace DotNetWebAPI.Services
{
    public interface IReviewRepository
    {
        Task<IReadOnlyList<Review>> GetAllReviewsAsync
           (ReviewSearchModel reviewSearchModel);
        Task<Review> GetReviewByIdAsync(int id);
        Task<Review> GetReviewByUserIdAsync(string id);
        Task AddReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(Review review);
        bool IsReviewExixtsAsync(int id);
    }
}
