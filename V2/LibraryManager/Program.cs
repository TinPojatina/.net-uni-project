// Program.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibraryManager.Data;
using LibraryManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
});


builder.Services.ConfigureApplicationCookie(options =>
{
    // How long until the cookie expires:
    options.ExpireTimeSpan = TimeSpan.FromDays(14);

    // If true, the cookie expiration time is reset each time the user visits the site:
    options.SlidingExpiration = true;

    // Paths to redirect on login/logout if using area=Identity with default scaffolded pages
    options.LoginPath = "/Home/Index";
    options.LogoutPath = "/Home/Index";
    options.AccessDeniedPath = "/Home/Index";
});
// Register custom services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Seed roles and users
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // 1. Create Roles
    string[] roles = { "Admin", "Librarian", "Member" };
    foreach (var role in roles)
    {
        if (!context.Roles.Any(r => r.Name == role))
        {
            context.Roles.Add(new IdentityRole { Name = role, NormalizedName = role.ToUpper() });
        }
    }
    await context.SaveChangesAsync();

    // 2. Create Admin User
    string adminEmail = "admin@library.com";
    string adminPassword = "admin123";
    if (!context.Users.Any(u => u.Email == adminEmail))
    {
        var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (result.Succeeded)
        {
            var createdAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (createdAdmin != null)
            {
                await userManager.AddToRoleAsync(createdAdmin, "Admin");
            }
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error creating admin user: {error.Description}");
            }
        }
    }

    // 3. Create Librarian User
    string librarianEmail = "librarian@library.com";
    string librarianPassword = "librarian123";
    if (!context.Users.Any(u => u.Email == librarianEmail))
    {
        var librarianUser = new IdentityUser { UserName = librarianEmail, Email = librarianEmail, EmailConfirmed = true };
        var result = await userManager.CreateAsync(librarianUser, librarianPassword);

        if (result.Succeeded)
        {
            var createdLibrarian = await userManager.FindByEmailAsync(librarianEmail);
            if (createdLibrarian != null)
            {
                await userManager.AddToRoleAsync(createdLibrarian, "Librarian");
            }
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error creating admin user: {error.Description}");
            }
        }
    }

    // 4. Create Member User
    string memberEmail = "member@library.com";
    string memberPassword = "member123";
    if (!context.Users.Any(u => u.Email == memberEmail))
    {
        var memberUser = new IdentityUser { UserName = memberEmail, Email = memberEmail, EmailConfirmed = true };
        var result = await userManager.CreateAsync(memberUser, memberPassword);

        if (result.Succeeded)
        {
            var createdMember = await userManager.FindByEmailAsync(memberEmail);
            if (createdMember != null)
            {
                await userManager.AddToRoleAsync(createdMember, "Member");
            }
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error creating admin user: {error.Description}");
            }
        }
    }
}

// Configure the HTTP request pipeline
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
