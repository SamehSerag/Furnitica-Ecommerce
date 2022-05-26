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
using DotNetWebAPI.DTOs;

namespace AngularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ShoppingDbContext _context;
        private readonly IWebHostEnvironment environment;
        private readonly IProductRepository productRepository;

        public ImagesController(ShoppingDbContext context, 
            IWebHostEnvironment _environment,
            IProductRepository _productRepository
            )
        {
            _context = context;
            environment = _environment;
            productRepository = _productRepository;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            return await _context.Images.ToListAsync();
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        // PUT: api/Images/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(int id, Image image)
        {
            if (id != image.Id)
            {
                return BadRequest();
            }

            _context.Entry(image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
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

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Image>> PostImage(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImage", new { id = image.Id }, image);
        }

        [HttpPost("AddImages")]
        public async Task<ActionResult<Product>> PostImagesToThereProducts(
           [FromForm] List<IFormFile> imagesFiles, [FromForm] int productId)
        {
            var files = Request.Form.Files;
            if (imagesFiles != null)
            {
                var folderPath = @"/images/product/";
                string folderPathWithRoot = environment.WebRootPath + folderPath;

                if (!Directory.Exists(folderPathWithRoot))
                {
                    Directory.CreateDirectory(folderPathWithRoot);
                }

                foreach (var image in imagesFiles)
                {
                    using (FileStream fileStream = System.IO.File.Create(
                        folderPathWithRoot + image.FileName))
                    {
                        image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    Image img = new Image();
                    img.ProductId = productId;
                    img.Src = folderPath + image.FileName;

                    _context.Images.Add(img);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(await productRepository.GetProductByIdAsync(productId));
            //return Ok();
        }


        [HttpPost("AddImages2")]
        public async Task<ActionResult<Product>> PostImagesToThereProducts2(
          /*[FromForm] TestDto test*/ [FromForm] ProductToAdd test)
        {
            var files = Request.Form.Files;
            //if (imagesFiles != null)
            //{
            //    var folderPath = @"/images/product/";
            //    string folderPathWithRoot = environment.WebRootPath + folderPath;

            //    if (!Directory.Exists(folderPathWithRoot))
            //    {
            //        Directory.CreateDirectory(folderPathWithRoot);
            //    }

            //    foreach (var image in imagesFiles)
            //    {
            //        using (FileStream fileStream = System.IO.File.Create(
            //            folderPathWithRoot + image.FileName))
            //        {
            //            image.CopyTo(fileStream);
            //            fileStream.Flush();
            //        }
            //        Image img = new Image();
            //        img.ProductId = productId;
            //        img.Src = folderPath + image.FileName;

            //        _context.Images.Add(img);
            //    }
            //}

            //await _context.SaveChangesAsync();

            return Ok(await productRepository.GetProductByIdAsync(1));
            //return Ok();
        }


        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}
