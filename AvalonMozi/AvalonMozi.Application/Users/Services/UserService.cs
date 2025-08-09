using AvalonMozi.Domain.Users;
using AvalonMozi.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AvalonMozi.Factories.UserFactories;
using AvalonMozi.Factories.UserFactories.Dto;

namespace AvalonMozi.Application.Users.Services
{
    public class UserService : IUserService
    {
        private readonly AvalonContext _database;
        private readonly IUserFactory _userFactory;
        public UserService(AvalonContext database, IUserFactory userFactory)
        {
            _database = database;
            _userFactory = userFactory;
        }

        public async Task<User> GetUser(string email)
        {
            return await _database.Users.FirstOrDefaultAsync(x=>x.Email == email);
        }

        public async Task<UserDto> AuthenticateUser(string email, string password)
        {
            var user = await _database.Users.Include(x=>x.Roles).Where(x => x.Email == email && HashPassword(password) == x.PasswordHash).FirstOrDefaultAsync();
            return _userFactory.ConvertEntityToDto(user);
        }

        public string HashPassword(string password)
        {
            var Password = password;
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
