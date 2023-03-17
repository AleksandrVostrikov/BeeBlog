using BeeBlog.Web.Models.ViewModels;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeeBlog.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        public List<UserView> DisplayedUsers { get; set; } = new();
        [BindProperty] public AddUser NewUser { get; set; }
        
        private readonly IUsersRepos _users;

        public IndexModel(IUsersRepos users)
        {
            _users = users;
        }
        public async Task<IActionResult> OnGet()
        {
            var allUsers = await _users.GetAllUsers();
            foreach (var us in allUsers)
            {
                DisplayedUsers.Add(new UserView
                {
                    Id = Guid.Parse(us.Id),
                    Email = us.Email,
                    UserName = us.UserName
                });
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var identyUser = new IdentityUser { 
                UserName = NewUser.UserName,
                Email = NewUser.Email,
            };
            var roles = new List<string>{ "User "};
            if (NewUser.IsAdmin)
            {
                roles.Add("Admin");
            }
            var result = await _users.Add(identyUser, NewUser.Password, roles);
            if (result)
            {
                return RedirectToPage("/admin/users/index");
            }
            return Page();
        }
    }
}
