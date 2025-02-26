using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Data;
using LibraryManager.Models;
using LibraryManager.Services; // Ensure this is added

var builder = WebApplication.CreateBuilder(args);

// Register Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Identity Services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure Application Cookie to Handle AJAX Login Requests Correctly
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/Account/Login"; // Default login page
//    options.AccessDeniedPath = "/Account/AccessDenied"; // Access denied page

//    options.Events.OnRedirectToLogin = context =>
//    {
//        if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
//        {
//            context.Response.StatusCode = 401; // Return 401 for AJAX requests
//        }
//        else
//        {
//            context.Response.Redirect(context.RedirectUri);
//        }
//        return Task.CompletedTask;
//    };
//});

// Register Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

// Register Application Services
builder.Services.AddScoped<IBookService, BookService>(); // Ensure this is added
builder.Services.AddScoped<IBorrowingService, BorrowingService>(); // Ensure BorrowingService is also registered
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedRolesAndAdmin(userManager, roleManager);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
