using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Models.ViewModels;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BeeBlog.Web.Pages.Admin.Posts
{
    [Authorize(Roles = "Admin")]
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
            string? notificationJson = TempData["Notification"] as string;
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

            BlogPosts = (await _postRepos.GetAllPostsAsync())?.ToList();
        }
    }
}
