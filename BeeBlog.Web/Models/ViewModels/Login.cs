using Duende.IdentityServer.Models;
using System.ComponentModel.DataAnnotations;

namespace BeeBlog.Web.Models.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = "Заполните имя пользователя")] public string UserName { get; set;}
        [Required(ErrorMessage = "Укажите пароль")] public string Password { get; set;}
    }
}
