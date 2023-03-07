using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeeBlog.Web.Pages.Tag
{
    public class DetailsModel : PageModel
    {
        private readonly IPostRepos _postRepos;
        public List<BlogPost> BlogPostList { get; set; }

        public DetailsModel(IPostRepos postRepos)
        {
            _postRepos = postRepos;
        }
        public async Task<IActionResult> OnGet(string tagName)
        {
            BlogPostList = (await _postRepos.GetAllPostsAsync(tagName)).ToList();
            return Page();
        }
    }
}
