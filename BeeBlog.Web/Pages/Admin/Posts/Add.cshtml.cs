using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Models.ViewModels;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BeeBlog.Web.Pages.Admin.Posts
{
    [Authorize(Roles = "Admin")]
    public class AddModel : PageModel
    {
        private readonly IPostRepos _postRepos;

        [BindProperty] public AddBlogPost AddBlogPostRequest { get; set; }
        [BindProperty] public IFormFile FeaturedImage { get; set; }
        [BindProperty] [Required(ErrorMessage ="���������� ����")] public String Tags { get; set; }

        public AddModel(IPostRepos postRepos)
        {
            _postRepos = postRepos;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            ValidateAddBlog();
            if (ModelState.IsValid)
            {
                var blogPost = new BlogPost()
                {
                    Heading = AddBlogPostRequest.Heading,
                    PageTitle = AddBlogPostRequest.PageTitle,
                    PageContent = AddBlogPostRequest.PageContent,
                    ShortDescription = AddBlogPostRequest.ShortDescription,
                    ImageURL = AddBlogPostRequest.ImageURL,
                    URLhandle = AddBlogPostRequest.URLhandle,
                    DateOfPublication = AddBlogPostRequest.DateOfPublication,
                    Author = AddBlogPostRequest.Author,
                    IsVisible = AddBlogPostRequest.IsVisible,
                    Tags = new List<Tags>(Tags.Split(',').Select(x => new Tags() { Name = x.Trim() }))
                };
                await _postRepos.AddAsync(blogPost);

                var notification = new Notification
                {
                    Message = "���������� �������!",
                    Type = Enums.NotificationType.Success
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/Admin/Posts/List");
            }
            return Page();
        }
        private void ValidateAddBlog()
        {
            if (AddBlogPostRequest.DateOfPublication.ToString() == "")
            {
                ModelState.AddModelError("AddBlogPostRequest.DateOfPublication", "�������� ������ ����");
            }
        }
    }
}
