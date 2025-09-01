using AvalonMozi.Application.Users.Dto;
using AvalonMozi.Domain.Users;
using AvalonMozi.Factories.UserFactories;
using AvalonMozi.Factories.UserFactories.Dto;
using AvalonMozi.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<User> GetUserByEmail(string email)
        {
            return await _database.Users.FirstOrDefaultAsync(x=>x.Email == email);
        }

        public async Task<User> GetUserByTechnicalId(string technicalID)
        {
            return await _database.Users.FirstOrDefaultAsync(x => x.TechnicalId == technicalID);
        }

        public async Task<UserDto> AuthenticateUser(string email, string password)
        {
            var user = await _database.Users.Include(x=>x.Roles).Where(x => x.Email == email && HashPassword(password) == x.PasswordHash).FirstOrDefaultAsync();
            if(user is null)
            {
                return null;
            }
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

        public async Task<bool> RegisterAccount(UserRegisterDto dto)
        {
            if(await _database.Users.AnyAsync(x=>x.Email == dto.Email))
            {
                return false;
            }

            var newUser = new User()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                PasswordHash = this.HashPassword(dto.Password),
                TechnicalId = Guid.NewGuid().ToString(),
                Roles = new List<Role>()
                {
                    await _database.Roles.FirstOrDefaultAsync(x=>x.TechnicalName == "CUSTOMER")
                },
                Deleted = false,
                LastSuccessfulLoginTime = new DateTime(1970,1,1)
            };

            var userOperation = await _database.Users.AddAsync(newUser);
            await _database.SaveChangesAsync();

            if(newUser.Id > 0)
            {
                return true;
            }

            return false;
        }
    }
}
