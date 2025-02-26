using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (model == null) return BadRequest();

        // Find user by email
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) return Unauthorized(); // Email doesn't exist

        // Check password
        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
        if (!result.Succeeded) return Unauthorized(); // Invalid password

        // Actually sign the user in
        await _signInManager.SignInAsync(user, isPersistent: false);
        return Ok();
    }
}

// For clarity, define a small model for login
public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
