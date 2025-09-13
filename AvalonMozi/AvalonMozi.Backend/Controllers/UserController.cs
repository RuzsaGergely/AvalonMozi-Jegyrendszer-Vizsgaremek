using AvalonMozi.Application.Users.Dto;
using AvalonMozi.Application.Users.Services;
using AvalonMozi.Factories.UserFactories;
using AvalonMozi.Factories.UserFactories.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvalonMozi.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<string> Login(string email, string password)
        {
            var user = await _userService.AuthenticateUser(email, password);
            if (user is not null)
            {
                var token = _jwtAuthManager.GenerateToken(user);
                return token;
            }

            return "ERROR_INVALID_USERNAME_OR_PASSWORD";
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var result = await _userService.RegisterAccount(dto);
            if(result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetUserProfile")]
        [Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER")]
        public async Task<UserDto> GetUserProfile()
        {
            string userTechnicalId = this.User.Claims.First(i => i.Type == "UserTechnicalId").Value;
            return _userFactory.ConvertEntityToDto(await _userService.GetUserByTechnicalId(userTechnicalId));
        }
    }
}
