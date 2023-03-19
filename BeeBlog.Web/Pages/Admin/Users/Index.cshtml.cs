using BeeBlog.Web.Models.ViewModels;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BeeBlog.Web.Pages.Admin.Users
{
    [Authorize(Roles ="Admin")]
    public class IndexModel : PageModel
    {
        public List<UserView> DisplayedUsers { get; set; } = new();
        [BindProperty] public AddUser NewUser { get; set; }
        [BindProperty] public Guid SelectedUserId { get; set; }

        private readonly IUsersRepos _users;

        public IndexModel(IUsersRepos users)
        {
            _users = users;
        }
        public async Task<IActionResult> OnGet()
        {
            await GetUsers();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var identyUser = new IdentityUser
                {
                    UserName = NewUser.UserName,
                    Email = NewUser.Email,
                };
                var roles = new List<string> { "User " };
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
            await GetUsers();
            return Page();

        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _users.DeleteUser(SelectedUserId);
            return RedirectToPage("/admin/users/index");
        }

        private async Task GetUsers()
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
        }
    }

}
