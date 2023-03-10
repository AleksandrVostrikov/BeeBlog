namespace BeeBlog.Web.Models.ViewModels
{
    public class AddBlogPost
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string PageContent { get; set; }
        public string ShortDescription { get; set; }
        public string ImageURL { get; set; }
        public string URLhandle { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }
    }
}
