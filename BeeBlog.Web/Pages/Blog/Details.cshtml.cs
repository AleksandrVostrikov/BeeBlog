using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeeBlog.Web.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly IPostRepos _postRepos;
        private readonly ILikesRepos _likesRepos;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogPost BlogPost { get; set; }
        public int TotalLikes { get; set; }
        public bool Liked { get; set; }


        public DetailsModel(
            IPostRepos postRepos, 
            ILikesRepos likesRepos,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _postRepos = postRepos;
            _likesRepos = likesRepos;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet(string URLhandle)
        {
            BlogPost = await _postRepos.GetPostAsync(URLhandle);
            if (BlogPost != null)
            {
                if (_signInManager.IsSignedIn(User))
                {
                    var likes = await _likesRepos.GetLikesBlogPost(BlogPost.Id);
                    var userId = _userManager.GetUserId(User);
                    Liked = likes.Any(x => x.UserId == Guid.Parse(userId));
                }
                TotalLikes = await _likesRepos.GetTotalLikes(BlogPost.Id);
            }

            return Page();
        }
    }
}
