using BeeBlog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeeBlog.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        [BindProperty] public Login LoginViewModel { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string? ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(
                LoginViewModel.UserName, LoginViewModel.Password, false, false);
                if (signInResult.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    {
                        return RedirectToPage(ReturnUrl);
                    }
                    return RedirectToPage("index");
                }
                else
                {
                    ViewData["Notification"] = new Notification
                    {
                        Message = "„то-то пошло не так, повторите попытку",
                        Type = Enums.NotificationType.Error
                    };
                    return Page();
                }
            }
            return Page();
        }
    }
}
