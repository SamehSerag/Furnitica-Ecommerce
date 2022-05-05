#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularProject.Data;
using AngularProject.Models;
using AngularAPI.Repository;
using AutoMapper;
using AngularAPI.Dtos;
using System.Text.Json;
using System.Diagnostics;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;
using AngularAPI.Data;
using AngularAPI.Models;

namespace AngularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IHostingEnvironment environment;

        private IProductRepository _productRepository { get; }

        public ProductsController(IProductRepository productRepository, 
            IMapper _mapper, IHostingEnvironment _environment)
        {
            _productRepository = productRepository;
            mapper = _mapper;
            environment = _environment;
        }

        [HttpGet("test")]
        public ActionResult<string> TestCategory([FromQuery] List<string> liststring)
        {


            return Ok(liststring);
        }

        [NonAction]
        public async Task<IReadOnlyList<Product>> GetProducts
            (ProductSearchModel productSearchModel)
        {
            var products = await _productRepository
                .GetAllProductsAsync(productSearchModel);

            // Pagination detials sent header
            PaginationMetaData paginationMetaData = 
                new PaginationMetaData(products.Count, productSearchModel.PageIndex,
                productSearchModel.PageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            PriceRange priceRangeObj = new PriceRange(products);
            Response.Headers.Add("X-PriceRange", JsonSerializer.Serialize(priceRangeObj));

            return products;

            //return Ok(await _productRepository.GetAllProductsAsync());
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByUser
         ([FromQuery] ProductSearchModel productSearchModel)
        {
            if (!productSearchModel.IsValidRange)
                return BadRequest("Max Price Less Than Min Price");

            var products = await GetProducts(productSearchModel);

            return Ok(
                mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>
                (products)
                );
        }
        // GET: api/Products/admin
        [HttpGet("admin/")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByAdmin
            ([FromQuery] ProductSearchModel productSearchModel)
        {
            if (!productSearchModel.IsValidRange)
                return BadRequest("Max Price Less Than Min Price");

            var products = await GetProducts(productSearchModel);

            return Ok(
                mapper.Map<IReadOnlyList<Product>, IReadOnlyList<AdminProductDto>>
                (products)
                );
        }


        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductByUser(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
          
            if (product == null)
            {
                return NotFound();
            }

            //return product;
            var productDto = mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(productDto);
        }

        [HttpGet("admin/{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductByAdmin(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            //return product;
            var productDto = mapper.Map<Product, AdminProductDto>(product);
            return Ok(productDto);
        }



        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("admin/{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            try
            {
                //await _context.SaveChangesAsync();
                await _productRepository.UpdateProductAsync(id, product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("admin/")]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            
            await _productRepository.AddProductAsync(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("admin/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteProductAsync(id);

            return NoContent();
        }

/*        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProductsByName(String name)
        {
            return await _context.Products
                .Where(p => p.Title_EN.Contains(name) || p.Title_AR.Contains(name))
                .Include(p => p.Image)
                .Include(p => p.Category)
                .ToListAsync();

        }
*/
        private bool ProductExists(int id)
        {
            return _productRepository.IsProductExixtsAsync(id);
        }



        //// Admin
        ///

        //[HttpGet("/sortAscndByPrice")]

        //public async Task<ActionResult<IEnumerable<Product>>> GetProductsSortedAscndByPrice(/*[FromBody] decimal min, [FromBody] decimal max*/)
        //{
        //    return await _context.Products
        //        .Include(p => p.Image)
        //        .Include(p => p.Category).OrderBy(p=>p.price).ToListAsync();
        //}

        //[HttpGet("/sortDescndByPrice")]

        //public async Task<ActionResult<IEnumerable<Product>>> GetProductsSortedDascndByPrice(/*[FromBody] decimal min, [FromBody] decimal max*/)
        //{
        //    return await _context.Products
        //        .Include(p => p.Image)
        //        .Include(p => p.Category).OrderByDescending(p => p.price).ToListAsync();
        //}
        //[HttpGet("/range")]

        //public async Task<ActionResult<IEnumerable<Product>>> GetProductsByPriceRange([FromBody] decimal min, [FromBody] decimal max)
        //{
        //    return await _context.Products
        //        .Include(p => p.Image)
        //        .Include(p => p.Category).Where(p=> p.price >= min && p.price <= max).ToListAsync();
        //}

        //[HttpGet("/admin")]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProductsByAdmin()
        //{
        //    return await _context.Products
        //        .Include(p => p.Image)
        //        .Include(p => p.Category)
        //        .Include(p => p.OrderProducts).ToListAsync();
        //}

        //[HttpGet("{id}/admin")]
        //public async Task<ActionResult<Product>> GetProductByAdmin(int id)
        //{
        //    var product = await _context.Products
        //        .Include(p => p.Image)
        //        .Include(p => p.Category)
        //        .Include(p => p.OrderProducts).FirstOrDefaultAsync(p => p.Id == id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return product;
        //}
    }
}
