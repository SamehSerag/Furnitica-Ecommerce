using AngularProject.Data;
using DotNetWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetWebAPI.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ShoppingDbContext _context;

        public ReviewRepository(ShoppingDbContext context)
        {
            this._context = context;
        }
        public async Task AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int id)
        {
            _context.Reviews.Remove(await GetReviewByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Review>> GetAllReviewsAsync(ReviewSearchModel reviewSearchModel)
        {
            IQueryable<Review> query = _context.Reviews.AsQueryable();

            if (reviewSearchModel != null)
            {

                if (!string.IsNullOrEmpty(reviewSearchModel.Sort))
                {
                    switch (reviewSearchModel.Sort)
                    {

                        case "DateAsc":
                            query = query.OrderBy(c => c.CreatedDate);
                            break;
                        case "DateDesc":
                            query = query.OrderByDescending(c => c.CreatedDate);
                            break;
                        case "StarsAsc":
                            query = query.OrderBy(c => c.starsCount);
                            break;
                        case "StarsDesc":
                            query = query.OrderByDescending(c => c.starsCount);
                            break;                        
                        default:
                            query = query.OrderByDescending(c => c.CreatedDate);
                            break;
                    }
                }
                if ((reviewSearchModel.PrdId != null))
                {
                    query = query
                   .Where(c => c.ProductId == reviewSearchModel.PrdId);

                }
            }

            return await query.ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews
                           .FirstOrDefaultAsync(c => c.Id == id);
        } 
        
        public async Task<Review> GetReviewByUserIdAsync(string id)
        {
            return await _context.Reviews
                           .FirstOrDefaultAsync(c => c.UserId == id);
        }

        public bool IsReviewExixtsAsync(int id)
        {
            return _context.Reviews
                           .Any(c => c.Id == id);
        }

        public async Task<Review> UpdateReviewAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            return review;
        }
    }
}
