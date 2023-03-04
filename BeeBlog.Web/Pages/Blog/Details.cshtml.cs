using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeeBlog.Web.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly IPostRepos _postRepos;

        public BlogPost BlogPost { get; set; }

        public DetailsModel(IPostRepos postRepos)
        {
            _postRepos = postRepos;
        }
        public async Task<IActionResult> OnGet(string URLhandle)
        {
            BlogPost = await _postRepos.GetPostAsync(URLhandle);
            return Page();
        }
    }
}
