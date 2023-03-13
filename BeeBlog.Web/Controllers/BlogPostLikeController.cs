using BeeBlog.Web.Models.ViewModels;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeeBlog.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostLikeController : Controller
    {
        private readonly ILikesRepos _likesRepos;

        public BlogPostLikeController(ILikesRepos likesRepos)
        {
            _likesRepos = likesRepos;
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            await _likesRepos.AddLike(addLikeRequest.BlogPostId, addLikeRequest.UserId);
            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikes([FromRoute] Guid blogPostId)
        {
            var totalLikes = await _likesRepos.GetTotalLikes(blogPostId);
            return Ok(totalLikes);
        }
    }
}
