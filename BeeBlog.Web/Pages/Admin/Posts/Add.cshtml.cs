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
        public async  Task<IActionResult> OnPost()
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
            await _beeBlogDbContext.BlogPosts.AddAsync(blogPost);
            await _beeBlogDbContext.SaveChangesAsync();

            return RedirectToPage("/Admin/Posts/List");
        }
    }
}
