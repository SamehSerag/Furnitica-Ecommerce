using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularProject.Data;
using DotNetWebAPI.Models;
using DotNetWebAPI.Services;
using AngularProject.Models;

namespace DotNetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListProductsController : ControllerBase
    {
        private readonly ShoppingDbContext _context;
        private readonly IWishListRepository _repo;

        public WishListProductsController(ShoppingDbContext context, IWishListRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/WishListProducts
        [HttpGet]
        public async Task<IEnumerable<WishListProduct>> GetWishListProducts()
        {
            User? user = HttpContext.Items["User"] as User;

            return await _repo.GetUserWishList(user.Id);
        }


        // POST: api/WishListProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{prdId}")]
        public async Task<ActionResult<WishListProduct>> AddToWishList(int prdId)
        {
         
            try
            {
                User? user = HttpContext.Items["User"] as User;

                _repo.AddToWishList(prdId, user.Id);

            }
            catch (DbUpdateException)
            {

                throw;

            }

            return RedirectToAction("GetWishListProducts");
        }



        // DELETE: api/WishListProducts/5
        [HttpDelete("{prdId}")]
        public async Task<IActionResult> RemoveFromWishList(int prdId)
        {
            if (WishListProductExists(prdId))
            {
                return NotFound();
            }

            User? user = HttpContext.Items["User"] as User;

            _repo.RemoveFromWishList(prdId, user.Id);

            return NoContent();
        }

        private bool WishListProductExists(int id)
        {
            User? user = HttpContext.Items["User"] as User;

            return _repo.WishListProductExists(id, user.Id);
        }
    }
}
