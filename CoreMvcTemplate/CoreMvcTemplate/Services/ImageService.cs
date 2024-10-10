using PdfiumViewer;
using System.Drawing;
using System.Drawing.Imaging;

namespace CoreMvcTemplate.Services
{
    public class ImageService
    {
        private readonly string[] _supportedImageTypes;

        public ImageService(IConfiguration configuration)
        {
            _supportedImageTypes = configuration.GetSection("ImageSettings:SupportedImageTypes").Get<string[]>();
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, string oldImageUrl)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Generate a unique file name for the new image
                string uniqueFileName = Guid.NewGuid().ToString() + imageFile.ContentType.Replace("image/", ".");
                string filePath = Path.Combine("wwwroot/images", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                   
                }

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(oldImageUrl))
                {
                    string oldImagePath = Path.Combine("wwwroot", oldImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                return "/images/" + uniqueFileName;
            }

            return oldImageUrl; // Return the old image URL if no new image was provided
        }


        public async Task<string> SaveFileAsync(IFormFile file,string seoName)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files");

            // Ensure the directory exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileExtension = Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, seoName + fileExtension);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }

        public async Task<string> SaveSiteImageAsync(IFormFile imageFile, string oldImageUrl,string name)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Generate a unique file name for the new image
                string uniqueFileName = name + imageFile.ContentType.Replace("image/", ".");
                string filePath = Path.Combine("wwwroot/site-images", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(oldImageUrl))
                {
                    string oldImagePath = Path.Combine("wwwroot", oldImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                return "/site-images/" + uniqueFileName;
            }

            return oldImageUrl; // Return the old image URL if no new image was provided
        }

        public void DeleteItemsImage(string path)
        {
            if(!string.IsNullOrEmpty(path))
            {
                string oldImagePath = Path.Combine("wwwroot", path.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
        }


        public void DeletePdfAndCoverImage(string FileSeoName)
        {

            var pdfPath = "/files/" + FileSeoName + ".pdf";
            if (!string.IsNullOrEmpty(pdfPath))
            {
                string oldImagePath = Path.Combine("wwwroot", pdfPath.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            var CoverImagePath = "/files/images/" + FileSeoName + ".png";
            if (!string.IsNullOrEmpty(CoverImagePath))
            {
                string oldCoverImagePath = Path.Combine("wwwroot", CoverImagePath.TrimStart('/'));
                if (System.IO.File.Exists(oldCoverImagePath))
                {
                    System.IO.File.Delete(oldCoverImagePath);
                }
            }
        }


        public async Task<string> SavePdfFirstPageAsImageAsync(IFormFile pdfFile, string imageFileName)
        {
            string imagePath = Path.Combine("wwwroot/files/images", imageFileName + ".png");

            using (var pdfStream = pdfFile.OpenReadStream())
            using (var pdfDocument = PdfDocument.Load(pdfStream))
            {
                var page = pdfDocument.Render(0, 300, 300, true);

                using (var image = new Bitmap(page.Width, page.Height))
                {
                    using (var graphics = Graphics.FromImage(image))
                    {
                        graphics.DrawImage(page, 0, 0, image.Width, image.Height);
                    }

                    image.Save(imagePath, ImageFormat.Png);
                }
            }

            return "/files/images/" + imageFileName + ".png";
        }


        public  bool ValidateImage(IFormFile file, int desiredWidth, int desiredHeight)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            // Check file type
            if (!_supportedImageTypes.Contains(file.ContentType.ToLower()))
            {
                return false;
            }
            var tolerance = 10;

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    using (var image = Image.FromStream(stream, false, false))
                    {
                        var height = image.Height;
                        var width = image.Width;

                        bool isWidthValid = Math.Abs(width - desiredWidth) <= tolerance;
                        bool isHeightValid = Math.Abs(height - desiredHeight) <= tolerance;

                        return isWidthValid && isHeightValid;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }


    }
}

