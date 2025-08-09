using AvalonMozi.Application.Users.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AvalonMozi.Factories.UserFactories.Dto;
using Microsoft.AspNetCore.Authorization;

namespace AvalonMozi.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthManager _jwtAuthManager;
        public AuthenticationController(IUserService userService, IJwtAuthManager jwtAuthManager)
        {
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
        }

        [HttpPost("login")]
        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userService.AuthenticateUser(email, password);
            if (user is not null)
            {
                var token = _jwtAuthManager.GenerateToken(user);
                return token;
            }

            return "ERROR_INVALID_USERNAME_OR_PASSWORD";
        }

        [HttpGet("TestAdmin")]
        [Authorize(Roles = "ADMIN")]
        public string TestAdmin()
        {
            return "Yeey";
        }

        [HttpGet("GeneratePassword")]
        public async Task<string> GeneratePassword(string password)
        {
            var user = await _userService.GetUser("vizsgaremek.admin@testdev.hu");
            return _userService.HashPassword(password);
        }
    }
}
