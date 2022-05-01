using AngularProject.Models;

namespace AngularAPI.Repository
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> UpdateProductAsync(int id, Product product);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(int id);
        bool IsProductExixtsAsync(int id);

    }
}
