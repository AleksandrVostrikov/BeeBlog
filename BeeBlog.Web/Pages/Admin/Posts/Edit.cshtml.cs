using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            await _postRepos.UpdateAsync(BlogPost);

            return RedirectToPage("/admin/posts/list");
        }
        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await _postRepos.DeleteAsync(BlogPost.Id);
            if (deleted)
            {
                return RedirectToPage("/admin/posts/list");
            }
            return Page();
        }
    }
}
