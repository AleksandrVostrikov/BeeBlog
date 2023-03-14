using Azure.Identity;

namespace BeeBlog.Web.Models.ViewModels
{
    public class BlogComment
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; } 
    }
}
