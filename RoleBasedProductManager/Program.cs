using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Set up the basic services
builder.Services.AddControllersWithViews();

// Hook up the database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Set up Identity with strong password rules
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add data protection for sensitive stuff
builder.Services.AddDataProtection();

var app = builder.Build();

// Set up the middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Set up some default users and roles
using (var scope = app.Services.CreateScope())
{
    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    
    await SetupDefaultUsersAndRoles(roleMgr, userMgr);
}

app.Run();

async Task SetupDefaultUsersAndRoles(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMgr)
{
    // Make sure we have the roles we need
    if (!await roleMgr.RoleExistsAsync("Admin"))
    {
        await roleMgr.CreateAsync(new IdentityRole("Admin"));
    }
    
    if (!await roleMgr.RoleExistsAsync("Manager"))
    {
        await roleMgr.CreateAsync(new IdentityRole("Manager"));
    }

    // Set up the admin user
    var adminUser = await userMgr.FindByNameAsync("admin");
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@example.com",
            EmailConfirmed = true
        };
        
        var createResult = await userMgr.CreateAsync(adminUser, "Admin@123");
        if (createResult.Succeeded)
        {
            await userMgr.AddToRoleAsync(adminUser, "Admin");
        }
    }

    // Set up the manager user
    var managerUser = await userMgr.FindByNameAsync("manager1");
    if (managerUser == null)
    {
        managerUser = new ApplicationUser
        {
            UserName = "manager1",
            Email = "manager1@example.com",
            EmailConfirmed = true
        };
        
        var createResult = await userMgr.CreateAsync(managerUser, "Manager@123");
        if (createResult.Succeeded)
        {
            await userMgr.AddToRoleAsync(managerUser, "Manager");
        }
    }
}
