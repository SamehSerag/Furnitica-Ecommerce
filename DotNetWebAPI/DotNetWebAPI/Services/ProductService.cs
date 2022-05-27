using AngularAPI.Data;
using AngularAPI.Dtos;
using AngularAPI.Models;
using AngularProject.Data;
using AngularProject.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AngularAPI.Repository
{
    public class ProductService : IProductRepository
    {
        private readonly ShoppingDbContext _context;
        //public static int TotalItems { get; } = 0;

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
            (ProductSearchModel productSearchModel)

        {

            IQueryable<Product> query = _context.Products.Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p => p.Owner)
                .Include(p => p.Reviews);

            if (productSearchModel != null)
            {
                if (productSearchModel.Category != null)
                {
                    Expression<Func<Product, bool>> predicate =
                         PredicateBuilder.New<Product>();

                    foreach (var category in productSearchModel.Category)
                    {
                        predicate = predicate.Or(p => p.CategoryID == category);
                    }

                    if (!(productSearchModel.Category.Count == 1
                        && productSearchModel.Category[0] == null))
                        query = query.Where(predicate);
                }

                if (!string.IsNullOrEmpty(productSearchModel.Search))
                {
                    query = query.Where(p =>
                        p.Title_EN.Contains(productSearchModel.Search) || p.Title_AR.Contains(productSearchModel.Search) ||
                        p.Details_EN.Contains(productSearchModel.Search) || p.Details_AR.Contains(productSearchModel.Search)
                    );
                }


                if (!string.IsNullOrEmpty(productSearchModel.Sortby))
                {

                    var propertyInfo = typeof(Product).GetProperty(productSearchModel.Sortby);
                    if (propertyInfo != null)
                    {
                        var param = Expression.Parameter(typeof(Product));
                        var expr = Expression.Lambda<Func<Product, object>>(
                              Expression.Convert(Expression.Property(param, propertyInfo), typeof(object)),
                              param
                               );

                        if (productSearchModel.Sortdir?.ToLower() == "dasc")
                            query = query.OrderByDescending(expr);
                        else
                            query = query.OrderBy(expr);
                    }
                }

                if (productSearchModel.MaxPrice != 0)
                    query = query.Where(p => p.price >= productSearchModel.MinPrice
                    && p.price <= productSearchModel.MaxPrice);

                if (productSearchModel.Color != null)
                    query = query.Where(p => p.Color == productSearchModel.Color);

                if (!string.IsNullOrEmpty(productSearchModel.OwnerId))
                {
                    query = query.Where(p => p.OwnerId
                                    == productSearchModel.OwnerId);
                }

               IProductRepository.TotalItems = query.Count();
               IProductRepository.MinPrice = query.Min(p => (int) p.price);
               IProductRepository.MaxPrice = query.Max(p => (int)p.price);

                query = query.Skip((productSearchModel.PageIndex - 1) * 
                    productSearchModel.PageSize).Take(productSearchModel.PageSize);
            }

            return await query.ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {

            //Product product = new Product() {Title_EN = "SS", Title_AR="ss", Details_EN="ss"
            //, Details_AR="dd", price=10, OwnerId ="1", Quantity=5, CategoryID =1, Color = Enums.Color.Green};
            //product.Images = new List<Image>();
            //product.Images.Add(new Image() { Src = "dddd" });

            //_context.Products.Add(product);
            //_context.SaveChanges();

            return await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Owner)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool IsProductExixtsAsync(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product;

        }
    }
}
