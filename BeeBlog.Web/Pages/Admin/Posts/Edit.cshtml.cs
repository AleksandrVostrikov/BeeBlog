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

        public void OnGet(Guid id)
        {
            BlogPost = _beeBlogDbContext.BlogPosts.Find(id);
        }

        public IActionResult OnPostEdit()
        {
            var existingBlogPost = _beeBlogDbContext.BlogPosts.Find(BlogPost.Id);
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
            _beeBlogDbContext.SaveChanges();
            return RedirectToPage("/admin/posts/list");
        }
        public IActionResult OnPostDelete()
        {
            var existingBlogPost = _beeBlogDbContext.BlogPosts.Find(BlogPost.Id);
            if (existingBlogPost != null)
            {
                _beeBlogDbContext.Remove(existingBlogPost);
            }
            _beeBlogDbContext.SaveChanges();
            return RedirectToPage("/admin/posts/list");
        }
    }
}
