using AvalonMozi.Factories.UserFactories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Users.Services
{
    public interface IJwtAuthManager
    {
        string GenerateToken(UserDto user);
        bool IsUserSuperAdmin(string token);
    }
}
