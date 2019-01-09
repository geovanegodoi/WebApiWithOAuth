using System;
using Microsoft.AspNetCore.Identity;
using WebApiWithOAuth.Context;

namespace WebApiWithOAuth.Security
{
    public class IdentityInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public static void Initialize(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            new IdentityInitializer(context, userManager, roleManager).Initialize();
        }

        private void Initialize()
        {
            if (_context.Database.EnsureCreated())
            {
                if (!_roleManager.RoleExistsAsync(Roles.ROLE_API_ADMIN).Result)
                {
                    if(_roleManager.CreateAsync(new IdentityRole(Roles.ROLE_API_ADMIN)).Result != IdentityResult.Success)
                    {
                        throw new Exception($"Error while creating the role {Roles.ROLE_API_ADMIN}");
                    }
                    if (_roleManager.CreateAsync(new IdentityRole(Roles.ROLE_API_NORMAL)).Result != IdentityResult.Success)
                    {
                        throw new Exception($"Error while creating the role {Roles.ROLE_API_NORMAL}");
                    }
                }

                CreateUser(new ApplicationUser
                {
                    UserName = "admin_api",
                    Email = "admin-api@mail.com",
                    EmailConfirmed = true
                }, "AdminApi01!", Roles.ROLE_API_ADMIN);

                CreateUser(new ApplicationUser
                {
                    UserName = "normal_api",
                    Email = "normal-api@mail.com",
                    EmailConfirmed = true
                }, "NormalApi02!", Roles.ROLE_API_NORMAL);

                CreateUser(new ApplicationUser
                {
                    UserName = "invalid_api",
                    Email = "invalid-api@mail.com",
                    EmailConfirmed = true
                }, "InvalidApi03!");
            }
        }

        private void CreateUser(
            ApplicationUser user,
            string password,
            string role = null)
        {
            if (_userManager.FindByIdAsync(user.UserName).Result == null)
            {
                var result = _userManager.CreateAsync(user, password).Result;

                if (result.Succeeded && !String.IsNullOrEmpty(role))
                {
                    _userManager.AddToRoleAsync(user, role).Wait();
                }
            }
        }
    }
}
