using AngularAPI.Models;
using AngularProject.Data;
using AngularProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAPI.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShoppingDbContext _context;

        public CategoryRepository(ShoppingDbContext context)
        {
            this._context = context;
        }
        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Category>> GetAllCategorysAsync(CategorySearchModel categorySearchModel)
        {
            return await ApplySpecifications(categorySearchModel);
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.AsQueryable().Include(x => x.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool IsCategoryExixtsAsync(int id)
        {
            return _context.Categories
                           .Any(c => c.Id == id);
        }

        public async Task<int> CountAsync(CategorySearchModel categorySearchModel)
        {
            return (await ApplySpecifications(categorySearchModel)).Count;
        }

        private async Task<IReadOnlyList<Category>> ApplySpecifications(CategorySearchModel categorySearchModel)
        {
            IQueryable<Category> query = _context.Categories.AsQueryable().Include(x => x.Products);

            if (categorySearchModel != null)
            {
                if (!string.IsNullOrEmpty(categorySearchModel.Search)) //name category
                {
                    query = query
                   .Where(c => c.Name.ToLower().Contains(categorySearchModel.Search.ToLower()));
                }

                if (!string.IsNullOrEmpty(categorySearchModel.Sort))
                {
                    switch (categorySearchModel.Sort)
                    {

                        case "nameAsc":
                            query = query.OrderBy(c => c.Name);
                            break;
                        case "nameDesc":
                            query = query.OrderByDescending(c => c.Name);
                            break;
                        default:
                            query = query.OrderBy(c => c.Name);
                            break;
                    }
                }
              /*  query = query.Skip((categorySearchModel.PageIndex - 1) *
                categorySearchModel.PageSize).Take(categorySearchModel.PageSize);*/
            }
            return await query.ToListAsync();
        }

 
    }
}
