using AvalonMozi.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Persistence
{
    public class DbInitializer
    {
        public static void InitializeAsync(AvalonContext context)
        {
            try
            {
                var pendingMigrations = context.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void TrySeedAsync(AvalonContext context, IConfiguration configuration)
        {
            var roles = context.Set<Role>().FirstOrDefault(x => x.TechnicalName == "ADMIN");
            if(roles == null)
            {
                context.Set<Role>().Add(new Role()
                {
                    TechnicalName = "ADMIN",
                    DisplayName = "Adminisztrátor",
                    Deleted = false
                });
                context.Set<Role>().Add(new Role()
                {
                    TechnicalName = "EMPLOYEE",
                    DisplayName = "Munkatárs",
                    Deleted = false
                });
                context.Set<Role>().Add(new Role()
                {
                    TechnicalName = "CUSTOMER",
                    DisplayName = "Vásárló",
                    Deleted = false
                });
                context.SaveChanges();
            }

            var adminUser = context.Set<User>().FirstOrDefault(x => x.Email == "vizsgaremek.admin@testdev.hu" && x.Roles.Any(x=>x.TechnicalName == "ADMIN"));
            if (adminUser == null)
            {
                context.Set<User>().Add(new User()
                {
                    FirstName = "Admin",
                    LastName = "Vizsgaremek",
                    Deleted = false,
                    Email = "vizsgaremek.admin@testdev.hu",
                    LastSuccessfulLoginTime = DateTime.Now,
                    PasswordHash = ""
                });
                context.SaveChanges();
            }
        }
    }
}
