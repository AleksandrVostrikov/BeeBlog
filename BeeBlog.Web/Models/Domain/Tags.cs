namespace BeeBlog.Web.Models.Domain
{
    public class Tags
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BlogPostId { get; set; }
    }
}
