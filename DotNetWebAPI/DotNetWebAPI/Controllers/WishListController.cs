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
using DotNetWebAPI.DTOs;
using AutoMapper;

namespace DotNetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly ShoppingDbContext _context;
        private readonly IWishListRepository _repo;
        private readonly IMapper _mapper;

        public WishListController(ShoppingDbContext context, IWishListRepository repo, IMapper mapper)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/WishListProducts
        [HttpGet]
        public async Task<IReadOnlyList<WishListDto>> GetWishListProducts()
        {
            /*User? user = HttpContext.Items["User"] as User;

            return await _repo.GetUserWishList(user.Id);*/
            var wishlist =  await _repo.GetUserWishList("2dec20bc-7da6-411b-999c-2f45e40c9e16");
            var wishlistDto = _mapper.Map<IReadOnlyList<WishListProduct>, IReadOnlyList<WishListDto>>(wishlist);
            return wishlistDto;
        }


        // POST: api/WishListProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{prdId}")]
        public async Task<ActionResult<WishListProduct>> AddToWishList(int prdId)
        {
         
            try
            {
               /* User? user = HttpContext.Items["User"] as User;

                _repo.AddToWishList(prdId, user.Id);*/
                _repo.AddToWishList(prdId, "2dec20bc-7da6-411b-999c-2f45e40c9e16");

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
            if (!WishListProductExists(prdId))
            {
                return NotFound();
            }

            /*User? user = HttpContext.Items["User"] as User;

            _repo.RemoveFromWishList(prdId, user.Id);*/
            _repo.RemoveFromWishList(prdId, "2dec20bc-7da6-411b-999c-2f45e40c9e16");

            return NoContent();
        }

        private bool WishListProductExists(int id)
        {
            /*User? user = HttpContext.Items["User"] as User;

            return _repo.WishListProductExists(id, user.Id);*/
            return _repo.WishListProductExists(id, "2dec20bc-7da6-411b-999c-2f45e40c9e16");
        }
    }
}
