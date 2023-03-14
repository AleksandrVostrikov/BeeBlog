using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeeBlog.Web.Repositories
{
    public class BlogPostCommentRepos : IBlogPostCommentRepos
    {
        private readonly BeeBlogDbContext _beeBlogDbContext;

        public BlogPostCommentRepos(BeeBlogDbContext beeBlogDbContext)
        {
            _beeBlogDbContext = beeBlogDbContext;
        }
        public async Task<BlogPostComment> AddCommentAsync(BlogPostComment blogPostComment)
        {
            await _beeBlogDbContext.BlogPostComment.AddAsync(blogPostComment);
            await _beeBlogDbContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetAllCommentAsync(Guid blogPostId)
        {
            return await _beeBlogDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
