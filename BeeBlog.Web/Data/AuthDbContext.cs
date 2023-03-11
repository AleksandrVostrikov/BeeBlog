using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeeBlog.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var superAdminRoleId = "de44722f-3cbe-40e4-9dbb-d9752418f78f";
            var adminRoleId = "6d5e4b09-8c47-4fc2-8b89-29002f2c8c31";
            var userRoleId = "a722779a-d692-462f-9f5c-831560e9cddb";

            //Roles (User, Admin, Super Admin)
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Name= "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole()
                {
                    Name= "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole()
                {
                    Name= "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);

            // Super Admin User
            var superAdminId = "ef4780a2-1047-42d3-a642-9f8ef88e6427";
            var superAdminUser = new IdentityUser()
            {
                Id = superAdminId,
                UserName = "DrevontSuper",
                NormalizedUserName = "DREVONTSUPER",
                Email = "bray@mail.ru",
                NormalizedEmail = "BRAY@MAIL.RU"

            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Bray3636@");
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add All roles to Super Admin User
            var superAdminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                },
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        }

    }
}
