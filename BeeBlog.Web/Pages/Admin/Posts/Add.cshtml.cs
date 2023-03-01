using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Models.ViewModels;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BeeBlog.Web.Pages.Admin.Posts
{
    public class AddModel : PageModel
    {
        private readonly IPostRepos _postRepos;

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }

        public AddModel(IPostRepos postRepos)
        {
            _postRepos = postRepos;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var blogPost = new BlogPost() {
                Heading = AddBlogPostRequest.Heading,
                PageTitle = AddBlogPostRequest.PageTitle,
                PageContent = AddBlogPostRequest.PageContent,
                ShortDescription = AddBlogPostRequest.ShortDescription,
                ImageURL = AddBlogPostRequest.ImageURL,
                URLhandle = AddBlogPostRequest.URLhandle,
                DateOfPublication = AddBlogPostRequest.DateOfPublication,
                Author = AddBlogPostRequest.Author,
                IsVisible = AddBlogPostRequest.IsVisible,
            };
            await _postRepos.AddAsync(blogPost);

            var notification = new Notification
            {
                Message = "New post created!",
                Type = Enums.NotificationType.Success
            };
            TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToPage("/Admin/Posts/List");
        }
    }
}
