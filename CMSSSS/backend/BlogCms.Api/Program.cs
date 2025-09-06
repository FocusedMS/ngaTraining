using System.Text;
using BlogCms.Api;
using BlogCms.Api.Data;
using BlogCms.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ---------- Limits ----------
builder.Services.Configure<FormOptions>(o => o.MultipartBodyLengthLimit = 100_000_000);

// ---------- Db ----------
builder.Services.AddDbContext<BlogDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// ---------- MVC + compression/caching ----------
builder.Services.AddControllers();
builder.Services.AddResponseCompression(opts =>
{
    opts.Providers.Add<GzipCompressionProvider>();
    opts.EnableForHttps = true;
});
builder.Services.AddResponseCaching();

// ---------- Swagger ----------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Blog CMS API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Bearer auth. Example: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// ---------- Auth ----------
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();

// ---------- CORS ----------
const string CorsPolicy = "Frontend";
builder.Services.AddCors(o =>
{
    o.AddPolicy(CorsPolicy, p =>
    {
        p.WithOrigins(
             builder.Configuration["Cors:FrontendOrigin"] ?? "http://localhost:5173",
             "http://127.0.0.1:5173")
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials()
         .WithExposedHeaders("ETag"); // so the client can read ETag
    });
});

// ---------- DI services ----------
builder.Services.AddSingleton<SeoService>();
builder.Services.AddScoped<JwtTokenService>();
// If you have a media service/controller, register it here:
// builder.Services.AddScoped<IMediaService, LocalFileMediaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ---------- Middleware order matters ----------
app.UseResponseCompression();
app.UseStaticFiles();

// CORS must be early, before auth & endpoints
app.UseCors(CorsPolicy);

app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// (Optional) ensure preflight OPTIONS always succeeds via CORS
// app.MapMethods("{*path}", new[] { "OPTIONS" }, () => Results.Ok()).RequireCors(CorsPolicy);

// ---------- Seed DB ----------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
    await SeedData.EnsureSeededAsync(db);
}

app.Run();
