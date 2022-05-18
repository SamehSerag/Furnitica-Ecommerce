using AngularProject.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DotNetWebAPI.Middlewares
{
    public class AuthMiddleware : IMiddleware
    {
        private readonly UserManager<User> userManager;
        public AuthMiddleware(UserManager<User> _userManager)
        {
            userManager = _userManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string authHeader = context.Request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                string tokenStr = authHeader.Substring("Bearer ".Length).Trim();

                System.Console.WriteLine(tokenStr);

                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadToken(tokenStr) as JwtSecurityToken;
                string userid = token!.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

                User user = await userManager.FindByIdAsync(userid);
                context.Items["User"] = user;
            }

            await next(context);
            }
    }
}
