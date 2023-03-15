using BeeBlog.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeeBlog.Web.Repositories
{
    public class UsersRepos : IUsersRepos
    {
        private readonly AuthDbContext _authDbContext;

        public UsersRepos(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }
        public async Task<IEnumerable<IdentityUser>> GetAllUsers()
        {
           return await _authDbContext.Users
                .Where(x => x.UserName != "DrevontSuper").ToListAsync();
        }
    }
}
