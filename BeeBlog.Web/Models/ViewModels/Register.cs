using System.ComponentModel.DataAnnotations;

namespace BeeBlog.Web.Models.ViewModels
{
    public class Register
    {
        [Required(ErrorMessage = "Заполните имя пользователя")] public string UserName { get; set; }
        
        [Required(ErrorMessage = "Укажите пароль")] [MinLength(6, 
            ErrorMessage = "Пароль должен быть не менее 6 символов")] public string Password { get; set; }
        [Required(ErrorMessage = "Заполните Email")] [EmailAddress] public string Email { get; set; }
    }
}
