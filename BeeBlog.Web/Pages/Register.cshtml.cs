using BeeBlog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeeBlog.Web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty] public Register RegisterViewModel { get; set; }
        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var user = new IdentityUser
            {
                UserName = RegisterViewModel.UserName,
                Email = RegisterViewModel.Email,
            };
            
            var identityResult = await _userManager.CreateAsync(user, RegisterViewModel.Password);
            if (identityResult.Succeeded)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Регистрация прошла успешно!",
                    Type = Enums.NotificationType.Success
                };
                return Page();
            }
            ViewData["Notification"] = new Notification
            {
                Message = "Произошла ошибка, повторите попытку!",
                Type = Enums.NotificationType.Error
            };
            return Page();
        }
    }
}
