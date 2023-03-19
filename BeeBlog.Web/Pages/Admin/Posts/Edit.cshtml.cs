using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Models.ViewModels;
using BeeBlog.Web.Repositories;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BeeBlog.Web.Pages.Admin.Posts
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IPostRepos _postRepos;

        [BindProperty] public EditBlogPostRequest BlogPost { get; set; }
        [BindProperty] public IFormFile FeaturedImage { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
         public string Tags { get; set; }

        public EditModel(IPostRepos postRepos)
        {
            _postRepos = postRepos;
        }

        public async Task OnGet(Guid id)
        {
            var blogPostDomain = await _postRepos.GetPostAsync(id);
            if (blogPostDomain != null && blogPostDomain.Tags != null)
            {
                BlogPost = new EditBlogPostRequest
                {
                    Id = blogPostDomain.Id,
                    Heading= blogPostDomain.Heading,
                    PageTitle= blogPostDomain.PageTitle,
                    PageContent= blogPostDomain.PageContent,
                    ShortDescription= blogPostDomain.ShortDescription,
                    ImageURL= blogPostDomain.ImageURL,
                    URLhandle= blogPostDomain.URLhandle,
                    DateOfPublication = blogPostDomain.DateOfPublication,
                    Author = blogPostDomain.Author,
                    IsVisible= blogPostDomain.IsVisible,
                };
                Tags = string.Join(',', blogPostDomain.Tags.Select(x => x.Name));
            }
        }

        public async Task<IActionResult> OnPostEdit()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var blogPostDomain = new BlogPost
                    {
                        Id = BlogPost.Id,
                        Heading = BlogPost.Heading,
                        PageTitle = BlogPost.PageTitle,
                        PageContent = BlogPost.PageContent,
                        ShortDescription = BlogPost.ShortDescription,
                        ImageURL = BlogPost.ImageURL,
                        URLhandle = BlogPost.URLhandle,
                        DateOfPublication = BlogPost.DateOfPublication,
                        Author = BlogPost.Author,
                        IsVisible = BlogPost.IsVisible,
                        Tags = new List<Tags>(Tags.Split(',').Select(x => new Tags { Name = x.Trim() }))
                    };
                    await _postRepos.UpdateAsync(blogPostDomain);
                    ViewData["Notification"] = new Notification
                    {
                        Message = "Изменения успешно сохранены!",
                        Type = Enums.NotificationType.Success
                    };
                }
                catch (Exception ex)
                {
                    ViewData["Notification"] = new Notification
                    {
                        Message = "Возникли ошибки",
                        Type = Enums.NotificationType.Error
                    };

                }
                return Page();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await _postRepos.DeleteAsync(BlogPost.Id);
            if (deleted)
            {
                var notification = new Notification
                {
                    Message = "Публикация удалена!",
                    Type = Enums.NotificationType.Success
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/admin/posts/list");
            }
            return Page();
        }
    }
}
