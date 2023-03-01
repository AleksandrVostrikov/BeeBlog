using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Models.ViewModels;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BeeBlog.Web.Pages.Admin.Posts
{
    public class EditModel : PageModel
    {
        private readonly IPostRepos _postRepos;

        [BindProperty]
        public BlogPost BlogPost { get; set; }

        public EditModel(IPostRepos postRepos)
        {
            _postRepos = postRepos;
        }

        public async Task OnGet(Guid id)
        {
            BlogPost = await _postRepos.GetPostAsync(id);
        }

        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                await _postRepos.UpdateAsync(BlogPost);
                ViewData["Notification"] = new Notification
                {
                    Message = "Record updated successfully!",
                    Type = Enums.NotificationType.Success
                };
            }
            catch (Exception ex)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Errors",
                    Type = Enums.NotificationType.Error
                };
                
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await _postRepos.DeleteAsync(BlogPost.Id);
            if (deleted)
            {
                var notification = new Notification
                {
                    Message = "Post deleted!",
                    Type = Enums.NotificationType.Success
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/admin/posts/list");
            }
            return Page();
        }
    }
}
