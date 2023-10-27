using Microsoft.AspNetCore.Mvc;
using NGWalksApplication;
using NGWalksDomain.ModelDTO;
using NGWalksDomain.Models;
using NGWalksDTOValidations;

namespace NGWalks.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _image;

        public ImageController(IImageRepository image)
        {
            _image = image;
        }
        //POST: ?api/Images/upload

        [HttpPost]
        [Route("Upload")]
        [ModelStateValidation]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            validatefileupload(imageUploadRequestDto);

            //convert DTo to Domain model
            var imageDomainModel = new Image
            {
                File = imageUploadRequestDto.File,
                FileDescription = imageUploadRequestDto.FileDescription,
                FileExtention = Path.GetExtension(imageUploadRequestDto.File.FileName),
                FileSizeInBytes = imageUploadRequestDto.File.Length,
                FileName = imageUploadRequestDto.File.FileName,
            };

            //User repository to upload image
            await  _image.Upload(imageDomainModel);

            return Ok(imageDomainModel);
        }

        private void validatefileupload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png"};

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "unsupported file extension");
            }
            if (imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", " file size is more than 10mb resize and try again");
            }
        }
    }
}
