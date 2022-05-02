using AngularProject.Data;
using AngularProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AngularAPI.Repository
{
    public class ProductService : IProductRepository
    {
        private readonly ShoppingDbContext _context;

        public ProductService(ShoppingDbContext context)
        {
            this._context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            _context.Products.Remove(await GetProductByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        //public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        //{
        //    return await _context.Products
        //    .Include(p => p.Images)
        //    .Include(p => p.Category).ToListAsync();
        //}
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync(string sortby, string sortdir)
        {

            var query = _context.Products.Include(p => p.Images)
                            .Include(p => p.Category);

            if (!string.IsNullOrEmpty(sortby))
            {

                var propertyInfo = typeof(Product).GetProperty(sortby);
                if(propertyInfo != null)
                {
                    var param = Expression.Parameter(typeof(Product));
                    var expr = Expression.Lambda < Func <Product, object> > (
                          Expression.Convert(Expression.Property(param, propertyInfo), typeof(object)),
                          param
                           );

                    if (sortdir.ToLower() == "asc")
                        return await query.OrderBy(expr)
                            .ToListAsync();
                    else
                        return await query.OrderByDescending(expr)
                            .ToListAsync();

                }

            }

            return await query
                    .ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool IsProductExixtsAsync(int id )
        {
            return  _context.Products.Any(p => p.Id == id);
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product;

        }
    }
}
