using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeeBlog.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPostRepos _postRepos;

        public List<BlogPost> BlogPostList { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IPostRepos postRepos)
        {
            _logger = logger;
            _postRepos = postRepos;
        }

        public async Task<IActionResult> OnGet()
        {
            BlogPostList = (await _postRepos.GetAllPostsAsync()).ToList();
            return Page();
        }
    }
}