using Medictionary.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Medictionary.Store.Interface;
using Medictionary.Services.Interfaces;
using Medictionary.Services;
using Medictionary.Helpers;
using Medictionary.Store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
var appConfigSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appConfigSection);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IStore<>), typeof(Store<>));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    options.LoginPath = "/Identity/Login";
    options.LogoutPath = "/Identity/Logout";
    options.AccessDeniedPath = "/Identity/AccessDenied";
    options.SlidingExpiration = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

SeedData(app);

app.Run();

void SeedData(IApplicationBuilder app)
{
    using (var scope = app.ApplicationServices.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new[] { "ADMIN", "User" };

        foreach (var role in roles)
        {
            if (!roleManager.RoleExistsAsync(role).Result)
            {
                roleManager.CreateAsync(new IdentityRole(role)).Wait();
            }
        }

        var adminUsername = "admin@gmail.com";
        var adminPassword = "password";

        var adminUser = userManager.FindByEmailAsync(adminUsername).Result;
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminUsername,
                EmailConfirmed = true
            };

            var result = userManager.CreateAsync(adminUser, adminPassword).Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(adminUser, "ADMIN").Wait();
            }
        }
        else
        {
            if (!userManager.IsInRoleAsync(adminUser, "ADMIN").Result)
            {
                userManager.AddToRoleAsync(adminUser, "ADMIN").Wait();
            }
        }
    }
}