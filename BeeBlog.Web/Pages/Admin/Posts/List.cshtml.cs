using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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

        public async Task OnGet()
        {
            BlogPosts = await _beeBlogDbContext.BlogPosts.ToListAsync();
        }
    }
}
