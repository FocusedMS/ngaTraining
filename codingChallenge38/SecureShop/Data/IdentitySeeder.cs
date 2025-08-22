using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SecureShop.Data;

public static class IdentitySeeder
{
	public static async Task SeedAsync(IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();
		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
		var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		await db.Database.MigrateAsync();

		string[] roles = ["Admin", "Customer"];
		foreach (var role in roles)
		{
			if (!await roleManager.RoleExistsAsync(role))
			{
				await roleManager.CreateAsync(new IdentityRole(role));
			}
		}

		var adminEmail = "admin@secureshop.local";
		var adminUser = await userManager.FindByEmailAsync(adminEmail);
		if (adminUser == null)
		{
			adminUser = new IdentityUser
			{
				UserName = adminEmail,
				Email = adminEmail,
				EmailConfirmed = true
			};
			var createResult = await userManager.CreateAsync(adminUser, "Admin#1234");
			if (createResult.Succeeded)
			{
				await userManager.AddToRoleAsync(adminUser, "Admin");
			}
		}
	}
}


