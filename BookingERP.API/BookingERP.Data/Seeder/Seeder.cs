using BookingERP.Common.Enums;
using BookingERP.Data.Context;
using BookingERP.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookingERP.Data.Seeder
{
    public class Seeder
    {
        private readonly BookingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public Seeder(BookingContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager; 
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            try
            {
                await SeedRolesAndAdminUserAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task SeedRolesAndAdminUserAsync()
        {
            if (!_context.Users.Any())
            {
                ApplicationUser admin = new()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                };

                await _roleManager.CreateAsync(new ApplicationRole { Name = Enums.UserRole.Admin.ToString() });
                await _roleManager.CreateAsync(new ApplicationRole { Name = Enums.UserRole.Guest.ToString() });
                await _roleManager.CreateAsync(new ApplicationRole { Name = Enums.UserRole.Manager.ToString() });

                var result = await _userManager.CreateAsync(admin, "admin");
                var resultRole = await _userManager.AddToRoleAsync(admin, "Admin");

                if (result != IdentityResult.Success || resultRole != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }
        }
    }
}
