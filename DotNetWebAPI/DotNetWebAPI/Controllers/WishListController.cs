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
using Microsoft.AspNetCore.Authorization;

namespace DotNetWebAPI.Controllers
{

    [Authorize(Roles = "Client")]
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly ShoppingDbContext _context;
        private readonly IWishListRepository _repo;
        private readonly IMapper _mapper;
        private readonly User? CurUser;
        public WishListController(ShoppingDbContext context, IWishListRepository repo, IMapper mapper)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
            CurUser = HttpContext.Items["User"] as User;
        }

        // GET: api/WishListProducts
        [HttpGet]
        public async Task<IReadOnlyList<WishListDto>> GetWishListProducts()
        {
            /*User? user = HttpContext.Items["User"] as User;

            return await _repo.GetUserWishList(user.Id);*/

            var wishlist =  await _repo.GetUserWishList("CurUser.Id");
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
                await _repo.AddToWishList(prdId, "CurUser.Id");

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
            await _repo.RemoveFromWishList(prdId, "CurUser.Id");

            return NoContent();
        }

        private bool WishListProductExists(int id)
        {
            /*User? user = HttpContext.Items["User"] as User;

            return _repo.WishListProductExists(id, user.Id);*/
            return _repo.WishListProductExists(id, "CurUser.Id");
        }
    }
}
