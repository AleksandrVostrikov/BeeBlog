using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BeeBlog.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageRepos _imageRepos;

        public ImagesController(IImageRepos imageRepos)
        {
            _imageRepos = imageRepos;
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageURL = await _imageRepos.UploadAsync(file);
            if (imageURL == null)
            {
                return Problem("Что-то пошло не так...", null, (int)HttpStatusCode.InternalServerError);
            }
            return Json(new { link = imageURL });


        }
    }
}
