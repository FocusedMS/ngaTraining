using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoleBasedProductManagement.Data;
using RoleBasedProductManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// ASP.NET Identity (password policy here)
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true; // special character
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Cookies: login + access denied paths
builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.LoginPath = "/Account/Login";
    opts.AccessDeniedPath = "/Account/AccessDenied";
});

// MVC
builder.Services.AddControllersWithViews();

// Data Protection (persist keys on disk)
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(
        Path.Combine(builder.Environment.ContentRootPath, "keys")))
    .SetApplicationName("RoleBasedProductManagement");

// HTTPS
builder.Services.AddHttpsRedirection(o => o.HttpsPort = 443);

// DI
builder.Services.AddScoped<IPriceProtector, PriceProtector>();

var app = builder.Build();

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

// Ensure database is created and migrated
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();
    
    // Seed roles + example users (uses UserManager for role assignment)
    await Seed.EnsureAsync(scope.ServiceProvider);
}

app.Run();

static class Seed
{
    public static async Task EnsureAsync(IServiceProvider sp)
    {
        var roleMgr = sp.GetRequiredService<RoleManager<IdentityRole>>();
        var userMgr = sp.GetRequiredService<UserManager<IdentityUser>>();

        foreach (var r in new[] { "Admin", "Manager" })
            if (!await roleMgr.RoleExistsAsync(r))
                await roleMgr.CreateAsync(new IdentityRole(r));

        // Admin user
        var adminEmail = "admin@demo.local";
        var admin = await userMgr.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            await userMgr.CreateAsync(admin, "Admin@123!");
            await userMgr.AddToRoleAsync(admin, "Admin"); // UserManager role assignment
        }

        // Manager user
        var mgrEmail = "manager@demo.local";
        var mgr = await userMgr.FindByEmailAsync(mgrEmail);
        if (mgr == null)
        {
            mgr = new IdentityUser { UserName = mgrEmail, Email = mgrEmail, EmailConfirmed = true };
            await userMgr.CreateAsync(mgr, "Manager@123!");
            await userMgr.AddToRoleAsync(mgr, "Manager"); // UserManager role assignment
        }
    }
}
