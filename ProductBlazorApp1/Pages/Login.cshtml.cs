using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductBlazorApp1.Data;

namespace ProductBlazorApp1.Pages;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public string ErrorMessage { get; set; }

    public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var user = await _userManager.FindByEmailAsync(Email);
        if (user == null)
        {
            ErrorMessage = "Kullanıcı bulunamadı.";
            return Page();
        }

        var result = await _signInManager.PasswordSignInAsync(
            user, Password, isPersistent: true, lockoutOnFailure: false);

        if (result.Succeeded)
        {

            // Rol/kullanıcıya göre yönlendirme
            if (Email.Equals("admin@gmail.com", StringComparison.OrdinalIgnoreCase))
                return Redirect("/admin");

            return Redirect("/panel");
        }

        ErrorMessage = "Giriş başarısız. Bilgileri kontrol edin.";
        return Page();
    }



}
