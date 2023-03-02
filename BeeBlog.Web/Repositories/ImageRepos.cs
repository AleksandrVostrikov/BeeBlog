using CloudinaryDotNet;

namespace BeeBlog.Web.Repositories
{
    public class ImageRepos : IImageRepos
    {
        private readonly Account _account;
        public ImageRepos(IConfiguration configuration)
        {
            _account = new Account(configuration.GetSection("Cloudinary")["Name"],
                configuration.GetSection("Cloudinary")["Key"], 
                configuration.GetSection("Cloudinary")["SecretKey"]);
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(_account);
            var uploadFileResult = await client.UploadAsync(
                new CloudinaryDotNet.Actions.ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    DisplayName = file.FileName,
                }
                );
            if (uploadFileResult != null && uploadFileResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadFileResult.SecureUri.ToString();
            }
            return null;
        }
    }
}
