using LibraryManager.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public static class DbInitializer
{
    public static async Task SeedRolesAndAdmin(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new IdentityRole("User"));

        var adminUser = await userManager.FindByEmailAsync("admin@library.com");
        if (adminUser == null)
        {
            var user = new ApplicationUser { UserName = "admin@library.com", Email = "admin@library.com" };
            var result = await userManager.CreateAsync(user, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
