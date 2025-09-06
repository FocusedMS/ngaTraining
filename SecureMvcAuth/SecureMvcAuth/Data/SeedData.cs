using Microsoft.AspNetCore.Identity;
using SecureMvcAuth.Models;

namespace SecureMvcAuth.Data;

public static class SeedData
{
    public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Create roles if they don't exist
        string[] roleNames = { "Admin", "User" };
        
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create admin user
        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser 
            { 
                UserName = "admin", 
                Email = "admin@example.com", 
                EmailConfirmed = true 
            };
            
            var createResult = await userManager.CreateAsync(adminUser, "Admin@123");
            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Create regular user
        var regularUser = await userManager.FindByNameAsync("user1");
        if (regularUser == null)
        {
            regularUser = new ApplicationUser 
            { 
                UserName = "user1", 
                Email = "user1@example.com", 
                EmailConfirmed = true 
            };
            
            var createResult = await userManager.CreateAsync(regularUser, "User@123");
            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(regularUser, "User");
            }
        }
    }
}
