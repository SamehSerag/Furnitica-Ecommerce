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

        public async Task DeleteReviewAsync(Review review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Review>> GetAllReviewsAsync(ReviewSearchModel reviewSearchModel)
        {
            return (await ApplySpecifications(reviewSearchModel)).ToList();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews.Include(x => x.user).ThenInclude(x => x.Image)
                           .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Review> GetReviewByUserIdAsync(string id,int prdid)
        {
            var review=  await _context.Reviews.Include(x => x.user).ThenInclude(x => x.Image)
                           .FirstOrDefaultAsync(c => c.ProductId == prdid &&  c.UserId==id);
            return review;
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

        public async Task<int> CountAsync(ReviewSearchModel reviewSearchModel)
        {
            reviewSearchModel.Sort = null;
            return (await ApplySpecifications(reviewSearchModel)).Count;
        }

        private async Task<IReadOnlyList<Review>> ApplySpecifications(ReviewSearchModel reviewSearchModel)
        {
            IQueryable<Review> query = _context.Reviews.AsQueryable().Include(x => x.user).ThenInclude(x => x.Image);

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
                if ((reviewSearchModel.stars != null))
                {
                    switch (reviewSearchModel.stars)
                    {
                        case 1:
                            query = query
                            .Where(c => c.starsCount == 1);
                            break;
                        case 2:
                            query = query
                            .Where(c => c.starsCount == 2);
                            break;
                        case 3:
                            query = query
                            .Where(c => c.starsCount == 3);
                            break;
                        case 4:
                            query = query
                            .Where(c => c.starsCount == 4);
                            break;
                        case 5:
                            query = query
                            .Where(c => c.starsCount == 5);
                            break;
                        default:
                            break;
                    }

                }
              
            }
            return await query.ToListAsync();
        }
    }
}
