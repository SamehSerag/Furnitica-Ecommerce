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
using DotNetWebAPI.DTOs;
using DotNetWebAPI.DTOs.Helpers;

namespace AngularAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private User? CurUser;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment environment;

        private IProductRepository _productRepository { get; }

        public ProductsController(IProductRepository productRepository, 
            IMapper _mapper, IHostingEnvironment _environment)
        {
            _productRepository = productRepository;
            mapper = _mapper;
            environment = _environment;
            //CurUser = HttpContext.Items["User"] as User ;

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
            //PaginationMetaData paginationMetaData = 
            //    new PaginationMetaData(products.Count, productSearchModel.PageIndex,
            //    productSearchModel.PageSize);
            //Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            PriceRange priceRangeObj = new PriceRange(products);
            Response.Headers.Add("X-PriceRange", JsonSerializer.Serialize(priceRangeObj));

            return products;

            //return Ok(await _productRepository.GetAllProductsAsync());
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<PaginationMetaData<ProductToReturnDto>>> GetProductsByUser
         ([FromQuery] ProductSearchModel productSearchModel)
        {
            if (!productSearchModel.IsValidRange)
                return BadRequest("Max Price Less Than Min Price");

            if(productSearchModel.OwnerId != null)
            {
                CurUser = HttpContext.Items["User"] as User;

                productSearchModel.OwnerId = CurUser.Id;    
            }

            var products = await GetProducts(productSearchModel);

            var productsDto = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>
                (products);
            PaginationMetaData<ProductToReturnDto> paginationMetaData =
                new PaginationMetaData<ProductToReturnDto>
                (IProductRepository.TotalItems, productSearchModel.PageIndex,
                productSearchModel.PageSize,
                productsDto);

            paginationMetaData.PriceRangeObj = new PriceRange(IProductRepository.MinPrice,IProductRepository.MaxPrice);
            return Ok(paginationMetaData);
        }
        // GET: api/Products/admin
        [HttpGet("owner/")]
        public async Task<ActionResult<AdminProductDto>> GetProductsByAdmin
            ([FromQuery] ProductSearchModel productSearchModel)
        {
            if (productSearchModel.OwnerId != null)
            {
                CurUser = HttpContext.Items["User"] as User;

                productSearchModel.OwnerId = CurUser.Id;
            }

            if (!productSearchModel.IsValidRange)
                return BadRequest("Max Price Less Than Min Price");

            var products = await GetProducts(productSearchModel);
            var adminProductDto = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<AdminProductDto>>
                (products);

            PaginationMetaData<AdminProductDto> paginationMetaData =
                new PaginationMetaData<AdminProductDto>
                (products.Count, productSearchModel.PageIndex,
                productSearchModel.PageSize,
                adminProductDto);

            paginationMetaData.PriceRangeObj = new PriceRange(IProductRepository.MinPrice, IProductRepository.MaxPrice);
            return Ok(paginationMetaData);
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

        [HttpGet("owner/{id}")]
        public async Task<ActionResult<AdminProductDto>> GetProductByAdmin(int id)
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
        [HttpPut("owner/{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductToAdd product)
        {
            //product.OwnerId = CurUser.Id;
            if (product.OwnerId != null)
            {
                CurUser = HttpContext.Items["User"] as User;

                product.OwnerId = CurUser.Id;
            }

            var productMapped = mapper.Map<ProductToAdd, Product>
               (product);
            if (id != productMapped.Id)
            {
                return BadRequest();
            }
            try
            {
                //await _context.SaveChangesAsync();
                await _productRepository.UpdateProductAsync(id, productMapped);
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

        // POST: api/Products/onwer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("owner")]
        public async Task<ActionResult<AdminProductDto>> PostProduct([FromForm]ProductToAdd product)
        {

            if (product.OwnerId != null)
            {
                CurUser = HttpContext.Items["User"] as User;

                product.OwnerId = CurUser.Id;
            }
            var productMapped = mapper.Map<ProductToAdd, Product>
                (product);

            /// Save Image Locally and then add path to database
            var Images = ImageSaver.SaveLocally(product.files, environment);
            productMapped.Images.AddRange(Images);

            await _productRepository.AddProductAsync(productMapped);

            return CreatedAtAction("GetProductByAdmin", new { id = productMapped.Id }, productMapped);
        }

        // DELETE: api/Products/owner/5
        [HttpDelete("owner/{id}")]
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
