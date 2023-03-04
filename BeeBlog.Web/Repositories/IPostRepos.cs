using BeeBlog.Web.Models.Domain;

namespace BeeBlog.Web.Repositories
{
    public interface IPostRepos
    {
        Task<IEnumerable<BlogPost>> GetAllPostsAsync();
        Task<BlogPost> GetPostAsync(Guid id);
        Task<BlogPost> GetPostAsync(string URLhandle);
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost> UpdateAsync(BlogPost blogPost);
        Task<bool> DeleteAsync(Guid id);
    }
}
