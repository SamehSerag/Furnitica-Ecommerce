using AngularAPI.DTOs;
using AngularAPI.Services;
using AngularProject.Data;
using AngularProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AngularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> UserManager;
        private readonly ShoppingDbContext context;

        public AuthController(UserManager<User> _UserManager, IConfiguration _configuration, ShoppingDbContext _context)
        {
            UserManager = _UserManager;
            configuration = _configuration;
            context = _context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto userDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);            

            User user = new();
            user.UserName = userDto.username;
            user.Email = userDto.email;
            user.Gender = userDto.Gender;
            user.Address = userDto.Address;


            User u = await UserManager.FindByEmailAsync(userDto.email);
            if (u != null)
                return BadRequest("email is already registered");            

            // this code will be replaced by cartrepository later
           /* Cart c = new Cart();
            context.Carts.Add(c);
            context.SaveChanges();
            user.CartID = c.Id;*/

            IdentityResult userCreated = await UserManager.CreateAsync(user, userDto.password);
            IdentityResult roleAssigned = await UserManager.AddToRolesAsync(user, new[]{ "Client" });

            if (userCreated.Succeeded && roleAssigned.Succeeded)
                return Ok();            
            else
            {
/*                context.Carts.Remove(c);
*/                context.SaveChanges();

                foreach (var item in userCreated.Errors)
                    ModelState.AddModelError(item.Code, item.Description);
                foreach (var item in roleAssigned.Errors)
                    ModelState.AddModelError(item.Code, item.Description);

                return BadRequest(ModelState);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            User user = await UserManager.FindByNameAsync(userDto.username);

            // return bad request if user is not found
            if (user == null)
                return BadRequest("this user is not registered");


            // return bad request if password does not match
            bool passwordMatch = await UserManager.CheckPasswordAsync(user, userDto.password);
            
            if (!passwordMatch)
                return BadRequest("username and password does not match");

            // create and send token to client

            // preparing claims to be added to token payload
            List<Claim> claims = new();
            claims.Add(new(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new(ClaimTypes.Name, user.UserName));
            claims.Add(new(ClaimTypes.Role, (await UserManager.GetRolesAsync(user)).First()));

            var roles = await UserManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new(ClaimTypes.Role, role));

            claims.Add(new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


            // creating the token from the claims
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            
            var jwtToken = new JwtSecurityToken(
                configuration["JWT:ValidIssuer"],
                configuration["JWT:ValidAudience"],
                claims,
                null,
                DateTime.Now.AddDays(365),
                signingCredentials: new(key, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = jwtToken.ValidTo  
            });
        }


        [Authorize]
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            return Ok();
        }

        [Authorize(Roles = "Client")]
        [HttpGet("testClient")]
        public IActionResult TestClient()
        {
            return Ok("client logged in success");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("testAdmin")]
        public IActionResult TestAdmin()
        {
            return Ok("Admin logged in success");
        }
    }
}
