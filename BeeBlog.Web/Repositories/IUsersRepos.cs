using Microsoft.AspNetCore.Identity;

namespace BeeBlog.Web.Repositories
{
    public interface IUsersRepos
    {
        Task<IEnumerable<IdentityUser>> GetAllUsers();
        Task<bool> Add(IdentityUser identityUser, string password, List<string> roles);
    }
}
