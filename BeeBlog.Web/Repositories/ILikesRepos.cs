using BeeBlog.Web.Models.Domain;

namespace BeeBlog.Web.Repositories
{
    public interface ILikesRepos
    {
        Task<int> GetTotalLikes(Guid blogPostId);
        Task AddLike(Guid blogPostId, Guid  usedId);
        Task<IEnumerable<Like>> GetLikesBlogPost(Guid blogPostId);
    }
}
