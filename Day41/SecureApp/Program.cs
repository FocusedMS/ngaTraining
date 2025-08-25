using Microsoft.EntityFrameworkCore;
using NWebsec.AspNetCore.Middleware;
using SecureApp.Data;
using SecureApp.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
	options.UseSqlServer(connectionString, sqlOptions =>
	{
		sqlOptions.EnableRetryOnFailure();
	});
});

// Options
builder.Services.Configure<CryptoOptions>(builder.Configuration.GetSection("Crypto"));

// Security services
builder.Services.AddScoped<IPasswordHasher, Pbkdf2PasswordHasher>();
builder.Services.AddScoped<IEncryptionService, AesGcmHmacEncryptionService>();

// Security headers (NWebsec)
builder.Services.AddHsts(options =>
{
	options.Preload = true;
	options.IncludeSubDomains = true;
	options.MaxAge = TimeSpan.FromDays(180);
});

var app = builder.Build();

// Security middleware
if (!app.Environment.IsDevelopment())
{
	app.UseHsts();
}
app.UseHttpsRedirection();

// Basic security headers
app.UseXContentTypeOptions();
app.UseXfo(options => options.Deny());
app.UseXXssProtection(options => options.EnabledWithBlockMode());
app.UseXDownloadOptions();
app.UseXRobotsTag(options => options.NoIndex().NoFollow());
app.UseReferrerPolicy(opts => opts.NoReferrer());
app.UseCsp(options => options
	.BlockAllMixedContent()
	.DefaultSources(s => s.Self())
	.ScriptSources(s => s.Self())
	.StyleSources(s => s.Self())
	.FrameAncestors(s => s.None())
);

// Minimal API for health
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

// OpenAPI in dev
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.MapControllers();

app.Run();
