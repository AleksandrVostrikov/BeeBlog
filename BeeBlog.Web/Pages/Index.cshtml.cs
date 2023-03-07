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
        private readonly ITagRepos _tagRepos;

        public List<BlogPost> BlogPostList { get; set; }
        public List<Tags> TagList { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IPostRepos postRepos, ITagRepos tagRepos)
        {
            _logger = logger;
            _postRepos = postRepos;
            _tagRepos = tagRepos;
        }

        public async Task<IActionResult> OnGet()
        {
            BlogPostList = (await _postRepos.GetAllPostsAsync()).ToList();
            TagList = (await _tagRepos.GetAllAsync()).ToList();
            return Page();
        }
    }
}