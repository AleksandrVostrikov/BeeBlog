namespace BeeBlog.Web.Repositories
{
    public interface IImageRepos
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
