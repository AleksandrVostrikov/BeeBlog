using BeeBlog.Web.Models.Domain;

namespace BeeBlog.Web.Repositories
{
    public interface IBlogPostCommentRepos
    {
        Task<BlogPostComment> AddCommentAsync(BlogPostComment blogPostComment);
        Task<IEnumerable<BlogPostComment>> GetAllCommentAsync(Guid blogPostId);

    }
}
