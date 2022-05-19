using AngularAPI.Models;
using AngularProject.Models;

namespace AngularAPI.Services
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyList<Category>> GetAllCategorysAsync
           (CategorySearchModel categorySearchModel);
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        bool IsCategoryExixtsAsync(int id);

        public Task<int> CountAsync(CategorySearchModel categorySearchModel);
    }
}
