using Medictionary.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Medictionary.Store.Interface;
using Medictionary.Services.Interfaces;
using Medictionary.Services;
using Medictionary.Helpers;
using Medictionary.Store;
using Medictionary.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var appConfigSection = builder.Configuration.GetSection("AppSettings");
var appConfig = appConfigSection.Get<AppSettings>();

// Configure PostgreSQL database connection.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(appConfig.ConnectionString));

// Configure Identity with PostgreSQL
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders(); 

// Register AppSettings
builder.Services.Configure<AppSettings>(appConfigSection);

// Register custom services
builder.Services.AddScoped(typeof(IStore<>), typeof(Store<>));
builder.Services.AddTransient<IAppSettings, AppSettings>();
builder.Services.AddControllersWithViews();

// Register Razor Pages services
builder.Services.AddRazorPages(); 

// Register FileService as Singleton
builder.Services.AddSingleton<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseAuthorization();

app.MapRazorPages(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
