using AngularAPI.Services;
using AngularProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo; 
        protected readonly UserManager<User> _userManager;


        public UserController( IUserRepository repo)
        {
            _repo = repo;   
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateInfo(User UpdatedUser)
        {
            try
            {
                await _repo.UpdateUserAsync(UpdatedUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(UpdatedUser.Id))
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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _repo.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        private bool UserExists(string id)
        {
            return _repo.IsUserExixtsAsync(id);
        }
    }


}
