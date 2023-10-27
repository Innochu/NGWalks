using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NGWalksApplication;
using NGWalksDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGWalksPersistence.Repository
{
    public class ImageRepo : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NGDbContext _nGDbContext;

        public ImageRepo(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NGDbContext nGDbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _nGDbContext = nGDbContext;
        }


        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtention}");

            //upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
;
            await image.File.CopyToAsync(stream);

            // https://localhost:1234/images/image.jpg


            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtention}";

            image.FilePath = urlFilePath;

            //add image to the Images table

            await _nGDbContext.Images.AddAsync(image);
            await _nGDbContext.SaveChangesAsync();

            return image;
        }
    }
}
