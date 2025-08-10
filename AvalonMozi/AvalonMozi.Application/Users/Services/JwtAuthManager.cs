using AvalonMozi.Factories.UserFactories.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Users.Services
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly IConfiguration _configuration;

        public JwtAuthManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserDto user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var claimList = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim("UserTechnicalId", user.TechnicalId)
            };

            foreach (var role in user.Roles)
            {
                claimList.Add(new Claim(ClaimTypes.Role, role.TechnicalName));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["DurationInMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public bool IsUserSuperAdmin(string token)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    TokenDecryptionKey = null
                };


                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);


                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);


                return roleClaim != null && roleClaim.Value == "Superadmin";
            }
            catch (Exception ex)
            {
                // Handle validation failure (token invalid, expired, etc.)
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return false;
            }
        }
    }
}
