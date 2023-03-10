using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Models.ViewModels;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BeeBlog.Web.Pages.Admin.Posts
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IPostRepos _postRepos;

        [BindProperty] public BlogPost BlogPost { get; set; }
        [BindProperty] public IFormFile FeaturedImage { get; set; }
        [BindProperty] public string Tags { get; set; }

        public EditModel(IPostRepos postRepos)
        {
            _postRepos = postRepos;
        }

        public async Task OnGet(Guid id)
        {
            BlogPost = await _postRepos.GetPostAsync(id);

            if (BlogPost != null && BlogPost.Tags != null)
            {
                Tags = string.Join(',', BlogPost.Tags.Select(x => x.Name));
            }
        }

        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                BlogPost.Tags = new List<Tags>(Tags.Split(',').Select(x => new Tags {Name = x.Trim()}));
                await _postRepos.UpdateAsync(BlogPost);
                ViewData["Notification"] = new Notification
                {
                    Message = "????????? ??????? ?????????!",
                    Type = Enums.NotificationType.Success
                };
            }
            catch (Exception ex)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "???????? ??????",
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
                    Message = "?????????? ???????!",
                    Type = Enums.NotificationType.Success
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/admin/posts/list");
            }
            return Page();
        }
    }
}
