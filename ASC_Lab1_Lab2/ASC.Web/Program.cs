using ASC.Business.Interfaces;
using ASC.Business.Services;
using ASC.DataAccess.Interfaces;
using ASC.DataAccess.Repositories;
using ASC.Web.Configuration;
using ASC.Web.Data;
using ASC.Web.Models;
using ASC.Web.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApplicationSettings>(
    builder.Configuration.GetSection(ApplicationSettings.SectionName));
builder.Services.Configure<SeedDataOptions>(
    builder.Configuration.GetSection(SeedDataOptions.SectionName));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=ASUSVIVOBOOK;Database=ASC_Lab1_Lab2_DB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure()));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<DbContext, ApplicationDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IMasterDataService, MasterDataService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddScoped<IIdentitySeed, IdentitySeed>();
builder.Services.AddScoped<IApplicationDataSeed, ApplicationDataSeed>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.EnsureCreatedAsync();

    var identitySeed = scope.ServiceProvider.GetRequiredService<IIdentitySeed>();
    await identitySeed.SeedAsync();

    var dataSeed = scope.ServiceProvider.GetRequiredService<IApplicationDataSeed>();
    await dataSeed.SeedAsync();
}

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

app.MapRazorPages();

app.Run();
