using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeeBlog.Web.Pages.Admin.Posts
{
    public class AddModel : PageModel
    {
        private readonly BeeBlogDbContext _beeBlogDbContext;

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }

        public AddModel(BeeBlogDbContext beeBlogDbContext)
        {
            _beeBlogDbContext = beeBlogDbContext;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var blogPost = new BlogPost() {
                Heading = AddBlogPostRequest.Heading,
                PageTitle = AddBlogPostRequest.PageTitle,
                PageContent = AddBlogPostRequest.PageContent,
                ShortDescription= AddBlogPostRequest.ShortDescription,
                ImageURL = AddBlogPostRequest.ImageURL,
                URLhandle = AddBlogPostRequest.URLhandle,
                DateOfPublication = AddBlogPostRequest.DateOfPublication,
                Author = AddBlogPostRequest.Author,
                IsVisible = AddBlogPostRequest.IsVisible,
            };
            _beeBlogDbContext.BlogPosts.Add(blogPost);
            _beeBlogDbContext.SaveChanges();

            return RedirectToPage("/Admin/Posts/List");
        }
    }
}
