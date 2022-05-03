using AngularAPI.Dtos;
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
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync
            (string sortby, string sortdir, int? category, string search,
            int pageIndex, int pageSize)
            
        {

            IQueryable<Product> query = _context.Products.Include(p => p.Images)
                .Include(p => p.Category);

            if (category != null)
            {
               query = query.Where(p => p.Category.Id == category);
            }

            if (search != null)
            {
                query = query.Where(p =>
                    p.Title_EN.Contains(search) || p.Title_AR.Contains(search) ||
                    p.Details_EN.Contains(search) || p.Details_AR.Contains(search)
                );
            }


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

                    if (sortdir?.ToLower() == "dasc")
                        query = query.OrderByDescending(expr);
                    else
                        query = query.OrderBy(expr);
                }
            }


            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
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
