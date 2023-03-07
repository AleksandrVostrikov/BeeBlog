using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeeBlog.Web.Repositories
{
    public class TagRepos : ITagRepos
    {
        private readonly BeeBlogDbContext _beeBlogDbContext;

        public TagRepos(BeeBlogDbContext beeBlogDbContext)
        {
            _beeBlogDbContext = beeBlogDbContext;
        }

        public async Task<IEnumerable<Tags>> GetAllAsync()
        {
            var tags = await _beeBlogDbContext.Tags.ToListAsync();
            return tags.DistinctBy(x => x.Name.ToLower());
        }
    }
}
