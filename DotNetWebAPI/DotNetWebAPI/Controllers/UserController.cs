using AngularAPI.Services;
using AngularProject.Models;
using AutoMapper;
using DotNetWebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        //protected readonly UserManager<User> _userManager;


        public UserController(IUserRepository repo, IMapper _mapper)
        {
            userRepository = repo;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            User? reqUser = HttpContext.Items["User"] as User;

            if (reqUser == null) return NotFound();

            User? user = await userRepository.GetByIdAsync(reqUser.Id);

            return Ok(mapper.Map<User, UserProfileDto>(user!));
        }

        [HttpPut]
        public async Task<ActionResult<User>> Put(UserProfileDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("invalid data!");
   
            User? user = HttpContext.Items["User"] as User;
            if (!userRepository.Exists(user!.Id))
                return NotFound("user not found");

            User tmpUser = (await userRepository.GetByEmailAsync(userDto.Email!))!;

            if (tmpUser != null && tmpUser.Id != user.Id)
                return BadRequest("mail already exists");


            User newUser = mapper.Map<UserProfileDto, User>(userDto);
            newUser.Id = user!.Id;

            user = await userRepository.UpdateAsync(newUser);
            return Ok(mapper.Map<User, UserProfileDto>(user));
        }

        
    }


}
