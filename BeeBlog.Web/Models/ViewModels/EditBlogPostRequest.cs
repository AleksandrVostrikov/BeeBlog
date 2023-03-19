using Duende.IdentityServer.Models;
using System.ComponentModel.DataAnnotations;

namespace BeeBlog.Web.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")] public Guid Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")] public string Heading { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")] public string PageTitle { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")] public string PageContent { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")] public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")] public string ImageURL { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")] public string URLhandle { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")] public DateTime DateOfPublication { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")] public string Author { get; set; }
        public bool IsVisible { get; set; }
    }
}
