using BeeBlog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeeBlog.Web.Data
{
    public class BeeBlogDbContext : DbContext
    {
        public BeeBlogDbContext(DbContextOptions<BeeBlogDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tags> Tags { get; set; }
    }
}
