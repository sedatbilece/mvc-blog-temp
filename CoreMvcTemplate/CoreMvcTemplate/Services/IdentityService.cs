using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Entities.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Services
{
    public class IdentityService
	{
		private readonly AppDbContext _dbContext;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;

		public IdentityService(AppDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			_dbContext = dbContext;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task EnsureAdminUser()
		{

			if (!_dbContext.Users.Any())
			{
				var adminUser = new User
				{
					UserName = "admin",
					Email = "admin@example.com"
				};


				var passwordFilePath = "password.txt";

				if (!File.Exists(passwordFilePath))
					File.WriteAllText(passwordFilePath, "");

				var passwordString = File.ReadAllText(passwordFilePath).Trim();


				var roleExists = await _roleManager.RoleExistsAsync("admin");

				if (!roleExists)
				{
					// Create the role
					var role = new Role();
					role.Name = "admin";
					await _roleManager.CreateAsync(role);
				}

				var result = await _userManager.CreateAsync(adminUser, passwordString);
				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(adminUser, "admin");
				}

			}
		}

		public void EnsureLandingPageExist()
		{
            if (!_dbContext.LandingPages.Any())
			{
				var landingPage = new LandingPage();
				landingPage.LogoUrl = "/site-images/logo.png";
				landingPage.FaviconUrl = "/site-images/favicon.png";
				_dbContext.LandingPages.Add(landingPage);
				_dbContext.SaveChanges();
			}

        }
	}
}
