using AvalonMozi.Application.Users.Dto;
using AvalonMozi.Domain.Users;
using AvalonMozi.Factories.UserFactories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Users.Services
{
    public interface IUserService
    {
        Task<User> GetUserByTechnicalId(string technicalID);
        Task<User> GetUserByEmail(string email);
        Task<UserDto> AuthenticateUser(string email, string password);
        string HashPassword(string password);
        Task<bool> RegisterAccount(UserRegisterDto dto);

    }
}
