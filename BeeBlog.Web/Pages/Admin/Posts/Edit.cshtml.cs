using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeeBlog.Web.Pages.Admin.Posts
{
    public class EditModel : PageModel
    {
        private readonly BeeBlogDbContext _beeBlogDbContext;
        [BindProperty]
        public BlogPost BlogPost { get; set; }

        public EditModel(BeeBlogDbContext beeBlogDbContext)
        {
            _beeBlogDbContext = beeBlogDbContext;
        }

        public async Task OnGet(Guid id)
        {
            BlogPost = await _beeBlogDbContext.BlogPosts.FindAsync(id);
        }

        public async Task<IActionResult> OnPostEdit()
        {
            var existingBlogPost = await _beeBlogDbContext.BlogPosts.FindAsync(BlogPost.Id);
            if (existingBlogPost != null)
            {
                existingBlogPost.Heading = BlogPost.Heading;
                existingBlogPost.PageTitle = BlogPost.PageTitle;
                existingBlogPost.PageContent = BlogPost.PageContent;
                existingBlogPost.ShortDescription = BlogPost.ShortDescription;
                existingBlogPost.ImageURL = BlogPost.ImageURL;
                existingBlogPost.PageTitle = BlogPost.PageTitle;
                existingBlogPost.URLhandle = BlogPost.URLhandle;
                existingBlogPost.DateOfPublication = BlogPost.DateOfPublication;
                existingBlogPost.Author = BlogPost.Author;
                existingBlogPost.IsVisible = BlogPost.IsVisible;
            }
            await _beeBlogDbContext.SaveChangesAsync();
            return RedirectToPage("/admin/posts/list");
        }
        public async Task<IActionResult> OnPostDelete()
        {
            var existingBlogPost = await _beeBlogDbContext.BlogPosts.FindAsync(BlogPost.Id);
            if (existingBlogPost != null)
            {
                _beeBlogDbContext.Remove(existingBlogPost);
            }
            await _beeBlogDbContext.SaveChangesAsync();
            return RedirectToPage("/admin/posts/list");
        }
    }
}
