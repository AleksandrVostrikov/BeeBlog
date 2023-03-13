namespace BeeBlog.Web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string PageContent { get; set; }
        public string ShortDescription { get; set;}
        public string ImageURL { get; set; }
        public string URLhandle { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }

        public ICollection<Tags> Tags { get;set; }
        public ICollection<Like> Likes { get; set; }
    }
}
