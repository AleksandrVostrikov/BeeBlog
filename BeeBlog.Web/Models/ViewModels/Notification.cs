using BeeBlog.Web.Enums;

namespace BeeBlog.Web.Models.ViewModels
{
    public class Notification
    {
        public string? Message { get; set; }
        public NotificationType Type { get; set; }
    }
}
