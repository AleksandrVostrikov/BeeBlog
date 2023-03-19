using BeeBlog.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeeBlog.Web.Repositories
{
    public class UsersRepos : IUsersRepos
    {
        private readonly AuthDbContext _authDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersRepos(
            AuthDbContext authDbContext,
            UserManager<IdentityUser> userManager)
        {
            _authDbContext = authDbContext;
            _userManager = userManager;
        }

        public async Task<bool> Add(IdentityUser identityUser, string password, List<string> roles)
        {
            var identityResult = await _userManager.CreateAsync(identityUser, password);
            
            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRolesAsync(identityUser, roles);
                if (identityResult.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task DeleteUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsers()
        {
           return await _authDbContext.Users
                .Where(x => x.UserName != "DrevontSuper").ToListAsync();
        }
    }
}
