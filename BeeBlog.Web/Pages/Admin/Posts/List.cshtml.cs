using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeeBlog.Web.Pages.Admin.Posts
{
    public class ListModel : PageModel
    {
        private readonly IPostRepos _postRepos;

        public List<BlogPost> BlogPosts { get; set; }

        public ListModel(IPostRepos postRepos)
        {
            _postRepos = postRepos;
        }

        public async Task OnGet()
        {
            BlogPosts = (await _postRepos.GetAllPostsAsync())?.ToList();
        }
    }
}
