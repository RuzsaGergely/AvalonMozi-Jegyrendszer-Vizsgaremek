using AvalonMozi.Application.Users.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AvalonMozi.Factories.UserFactories.Dto;
using Microsoft.AspNetCore.Authorization;

namespace AvalonMozi.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN,EMPLOYEE")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthManager _jwtAuthManager;
        public AuthenticationController(IUserService userService, IJwtAuthManager jwtAuthManager)
        {
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userService.AuthenticateUser(email, password);
            if (user is not null)
            {
                var token = _jwtAuthManager.GenerateToken(user);
                return Ok(token);
            }

            return Unauthorized("ERROR_INVALID_USERNAME_OR_PASSWORD");
        }

        [HttpGet("GeneratePassword")]
        public async Task<IActionResult> GeneratePassword(string password)
        {
            var user = await _userService.GetUser("vizsgaremek.admin@testdev.hu");
            if(user is not null)
            {
                return Ok(_userService.HashPassword(password));
            }
            return BadRequest();
        }
    }
}
