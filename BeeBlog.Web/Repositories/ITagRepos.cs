using BeeBlog.Web.Models.Domain;

namespace BeeBlog.Web.Repositories
{
    public interface ITagRepos
    {
        Task<IEnumerable<Tags>> GetAllAsync();
    }
}
