using System;
using System.Linq;
using CBA.Data;
using CBA.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CBA.DATA
{
    public class AppUserSeedData
    {
        private readonly AppDbContext context;

        public AppUserSeedData(AppDbContext _context)
        {
            context = _context;
        }

        public async Task SeedAdminUserAndRoles()
        {
            string[] roles = new string[]
            {
                 "admin",
                 "tester",
                 "manager",
                 "auditor",
                 "developer"
            };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole { Name = role, NormalizedName = role });
                }
            }

            var user = new ApplicationUser
            {
                FirstName = "Ejezie",
                LastName = "Chinedu",
                Email = "dreecast07@gmail.com",
                NormalizedEmail = "DREECAST07@GMAIL.COM",
                UserName = "superadmin",
                NormalizedUserName = "SUPERADMIN",
                PhoneNumber = "+2348142074224",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "admin");
            }

            await context.SaveChangesAsync();

        }
    }

}

