using BeeBlog.Web.Models.Domain;
using BeeBlog.Web.Models.ViewModels;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BeeBlog.Web.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly IPostRepos _postRepos;
        private readonly ILikesRepos _likesRepos;
        private readonly IBlogPostCommentRepos _blogPostCommentRepos;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogPost BlogPost { get; set; }
        public int TotalLikes { get; set; }
        public bool Liked { get; set; }
        public List<BlogComment> Comments { get; set; } = new();
        
        [BindProperty] public Guid BlogPostId { get; set; }
        [Required(ErrorMessage = "Комментарий пуст")]
        [MinLength(1, ErrorMessage ="Комментарий должен содержать больше одного символа")]
        [BindProperty] public string BlogPostComment { get; set; }

        public DetailsModel(
            IPostRepos postRepos, 
            ILikesRepos likesRepos,
            IBlogPostCommentRepos postCommentRepos,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _postRepos = postRepos;
            _likesRepos = likesRepos;
            _blogPostCommentRepos = postCommentRepos;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet(string URLhandle)
        {
            await GetPost(URLhandle);
            return Page();
        }

        public async Task<IActionResult> OnPost(string URLhandle)
        {
            if (ModelState.IsValid)
            {
                if (_signInManager.IsSignedIn(User) && BlogPostComment.Count() > 0)
                {
                    var comment = new BlogPostComment()
                    {
                        BlogPostId = BlogPostId,
                        UserId = Guid.Parse(_userManager.GetUserId(User)),
                        Description = BlogPostComment,
                        CreatedDate = DateTime.Now
                    };
                    await _blogPostCommentRepos.AddCommentAsync(comment);
                }
                return RedirectToPage("/blog/details", new { urlhandle = URLhandle });
            }
            await GetPost(URLhandle);
            return Page();
            
        }

        private async Task GetComments()
        {
            var blogPostComments = await _blogPostCommentRepos.GetAllCommentAsync(BlogPostId);
            
            foreach (var bpc in blogPostComments)
            {
                Comments.Add(new BlogComment()
                {
                    Description = bpc.Description,
                    CreatedDate = bpc.CreatedDate,
                    UserName = (await _userManager.FindByIdAsync(bpc.UserId.ToString())).UserName
                });
            }
        }

        private async Task GetPost(string URLhandle)
        {
            BlogPost = await _postRepos.GetPostAsync(URLhandle);
            if (BlogPost != null)
            {
                BlogPostId = BlogPost.Id;
                if (_signInManager.IsSignedIn(User))
                {
                    var likes = await _likesRepos.GetLikesBlogPost(BlogPost.Id);
                    var userId = _userManager.GetUserId(User);
                    Liked = likes.Any(x => x.UserId == Guid.Parse(userId));
                    await GetComments();
                }
                TotalLikes = await _likesRepos.GetTotalLikes(BlogPost.Id);
            }
        }
    }
}
