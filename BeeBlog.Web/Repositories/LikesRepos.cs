using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeeBlog.Web.Repositories
{
    public class LikesRepos : ILikesRepos
    {
        private readonly BeeBlogDbContext _beeBlogDbContext;

        public LikesRepos(BeeBlogDbContext beeBlogDbContext)
        {
            _beeBlogDbContext = beeBlogDbContext;
        }

        public async Task AddLike(Guid blogPostId, Guid usedId)
        {
            var like = new Like
            {
                Id = Guid.NewGuid(),
                BlogPostId = blogPostId,
                UserId = usedId
            };
            await _beeBlogDbContext.Like.AddAsync(like);
            await _beeBlogDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Like>> GetLikesBlogPost(Guid blogPostId)
        {
            return await _beeBlogDbContext.Like.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await _beeBlogDbContext.Like.CountAsync(x => x.BlogPostId == blogPostId);
            
        }
    }
}
