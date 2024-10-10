using CoreMvcTemplate.Entities.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace CoreMvcTemplate.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _dbContext;

        public UploadController(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }


        // CkEditor5 iamge upload için özel endpoint
        [HttpPost]
        [Route("upload/image")]
        public async Task<IActionResult> UploadImage(IFormFile upload)
        {
            if (upload != null && upload.Length > 0)
            {

                string filePath = Path.Combine("wwwroot/ckeditorfiles",upload.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await upload.CopyToAsync(stream);
                }

                var request = _httpContextAccessor.HttpContext.Request;
                string host = request.Scheme + "://" + request.Host;
                var refererUrl = Request.Headers["Referer"].ToString();

                // URL oluştur
                var url = host + "/ckeditorfiles/" + upload.FileName;

                _dbContext.UploadImageLogs.Add(new Entities.UploadImageLog { RequestUrl = refererUrl ,ImagePath = url });
                _dbContext.SaveChanges();

                return new JsonResult(new
                {
                    uploaded = true,
                    url = url
                });
            }
            return new JsonResult(new { error = new { message = "Upload failed" } });
        }




    }
}
