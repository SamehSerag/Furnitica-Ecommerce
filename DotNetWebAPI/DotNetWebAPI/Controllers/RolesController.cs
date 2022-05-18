using DotNetWebAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RolesController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(RoleDto role)
        {
            if (!ModelState.IsValid)
                return BadRequest("Role Name Can Not Be Empty.");

            var r = roleManager.Roles.FirstOrDefault(r => r.Name == role.RoleName);
            if (r != null)
                return BadRequest("Role Already Exists");

            IdentityResult RoleCreated = await roleManager.CreateAsync(new IdentityRole(role.RoleName));

            if(!RoleCreated.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, "error creating new role " + role.RoleName);

            return Created("", new{ roleName = role.RoleName });
        }

    }
}
