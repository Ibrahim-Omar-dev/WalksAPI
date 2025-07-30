using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WalksAPI.Model.DTOs.Image;
using WalksAPI.Models.Domain;
using WalksAPI.RepositoryUntionOfWork;

namespace WalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ImageController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequest imageUploadRequest)
        {
            ValidateImageUploadRequest(imageUploadRequest);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //convert ImageUploadRequest to Image domain model
            var image = new Image
            {
                File = imageUploadRequest.File,
                FileName=imageUploadRequest.FileName,
                FileExtension = Path.GetExtension(imageUploadRequest.File.FileName),
                FileDescription = imageUploadRequest.FileDescription,
                FileSizeInBytes = imageUploadRequest.File.Length
            };
            await unitOfWork.ImageRepository.UploadImage(image);
            await unitOfWork.SaveAsync();
            return Ok(new { message = "Image uploaded successfully", image });
        }
        [NonAction]
        public void ValidateImageUploadRequest(ImageUploadRequest imageUploadRequest)
        {
            string extension = Path.GetExtension(imageUploadRequest.File.FileName).ToLower();
            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("File", "Invalid file type. Allowed types are: " + string.Join(", ", allowedExtensions));
            }

            if (imageUploadRequest.File.Length > 10 * 1024 * 1024) // 10 MB
            {
                ModelState.AddModelError("File", "File size exceeds the maximum limit of 10 MB.");
            }
        }

    }
}
