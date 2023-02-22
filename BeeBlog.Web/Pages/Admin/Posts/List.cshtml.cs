using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeeBlog.Web.Pages.Admin.Posts
{
    public class ListModel : PageModel
    {
        private readonly BeeBlogDbContext _beeBlogDbContext;
        public List<BlogPost> BlogPosts { get; set; }

        public ListModel(BeeBlogDbContext beeBlogDbContext)
        {
            _beeBlogDbContext = beeBlogDbContext;
        }

        public void OnGet()
        {
            BlogPosts = _beeBlogDbContext.BlogPosts.ToList();
        }
    }
}
