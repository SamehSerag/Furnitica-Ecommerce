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

namespace AngularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository { get; }

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _productRepository.GetAllProductsAsync());
        }
        

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            //return product;
            return Ok(product);
        }
       


        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _productRepository.AddProductAsync(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
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
