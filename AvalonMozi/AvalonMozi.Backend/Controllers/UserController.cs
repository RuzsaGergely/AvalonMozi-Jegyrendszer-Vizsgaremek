using AvalonMozi.Application.Users.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AvalonMozi.Factories.UserFactories.Dto;
using Microsoft.AspNetCore.Authorization;
using AvalonMozi.Factories.UserFactories;

namespace AvalonMozi.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN,EMPLOYEE")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserFactory _userFactory;
        private readonly IJwtAuthManager _jwtAuthManager;
        public UserController(IUserService userService, IJwtAuthManager jwtAuthManager, IUserFactory userFactory)
        {
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
            _userFactory = userFactory;
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

        [HttpGet("GetUserProfile")]
        [Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER")]
        public async Task<UserDto> GetUserProfile()
        {
            string userTechnicalId = this.User.Claims.First(i => i.Type == "UserTechnicalId").Value;
            return _userFactory.ConvertEntityToDto(await _userService.GetUserByTechnicalId(userTechnicalId));
        }

        [HttpGet("GeneratePassword")]
        public async Task<IActionResult> GeneratePassword(string password)
        {
            var user = await _userService.GetUserByEmail("vizsgaremek.admin@testdev.hu");
            if(user is not null)
            {
                return Ok(_userService.HashPassword(password));
            }
            return BadRequest();
        }
    }
}
