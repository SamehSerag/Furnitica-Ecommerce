using AngularAPI.DTOs;
using DotNetWebAPI.DTOs;
using AngularAPI.Services;
using AngularProject.Data;
using AngularProject.Models;
using AutoMapper;
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
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthController(UserManager<User> _UserManager, RoleManager<IdentityRole> _roleManager, IConfiguration _configuration, ICartRepository _cartRepository, IMapper _mapper)
        {
            UserManager = _UserManager;
            roleManager = _roleManager;
            configuration = _configuration;
            cartRepository = _cartRepository;
            mapper = _mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto userDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            IdentityRole role = await roleManager.FindByNameAsync(userDto.Role);
            if (role == null)
                return BadRequest("Invalid Role");

            try
            {
                User user = await CreateUser(userDto);
                var jwtToken = await GenerateToken(user);

                return Created("", new
                {
                    userData = mapper.Map<User, UserProfileDto>(user),
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo.ToString(),
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

            var jwtToken = await GenerateToken(user);

            return Ok(new {
                userData = mapper.Map<User, UserProfileDto>(user),
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = jwtToken.ValidTo.ToString(),  
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
            return Ok();
        }


        [NonAction]
        private async Task<User> CreateUser(RegisterDto userDto)
        {
            User user = mapper.Map<RegisterDto, User>(userDto);

            User u = await UserManager.FindByEmailAsync(userDto.email);
            if (u != null)
                throw new Exception("email is already registered");


            IdentityResult userCreated = await UserManager.CreateAsync(user, userDto.password);
            IdentityResult roleAssigned = await UserManager.AddToRolesAsync(user, new[] { userDto.Role });


            if (userCreated.Succeeded && roleAssigned.Succeeded)
            {
                if(userDto.Role == "Client")
                    cartRepository.GetCart(user.Id);               
                return user;
            }
            else
            {
                foreach (var item in userCreated.Errors)
                    ModelState.AddModelError(item.Code, item.Description);
                foreach (var item in roleAssigned.Errors)
                    ModelState.AddModelError(item.Code, item.Description);

                var errorList = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList();
                throw new Exception(string.Join('#', errorList));
            }
        }


        [NonAction]
        private async Task<JwtSecurityToken> GenerateToken(User user)
        {
            // preparing claims to be added to token payload
            List<Claim> claims = new();
            claims.Add(new(ClaimTypes.NameIdentifier, user.Id));
            //claims.Add(new(ClaimTypes.Name, user.UserName));
            //claims.Add(new(ClaimTypes.Role, (await UserManager.GetRolesAsync(user)).First()));

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

            return jwtToken;
        }

    }
}
