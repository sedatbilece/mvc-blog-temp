using AutoMapper;
using Azure.Core;
using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Helpers;
using CoreMvcTemplate.Models.PdfFile;
using CoreMvcTemplate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Controllers
{
    public class PdfViewerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PdfViewerController(AppDbContext context, ImageService imageService, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _imageService = imageService;
            _mapper = mapper;
            _configuration = configuration;
        }

        public IActionResult Index()
        {

            var model = _context.PdfFiles.AsNoTracking().Where(x=>x.isActive).OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).ToList();
            return View(model);
        }

        public IActionResult Detail(string title)
        {
            if (string.IsNullOrEmpty(title))
                return RedirectToAction("Index", "Home");

            var licenceKey = _configuration["LicenceKey"]; 
            TempData["LicenceKey"] = licenceKey;

            var file = _context.PdfFiles.FirstOrDefault(x=>x.isActive && x.SeoName == title);
            
            return View(file);
        }


        [Authorize]
        public IActionResult List()
        {
            var model =  _context.PdfFiles.AsNoTracking().OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).ToList();
            TempData["CopyUrlHostString"] =  $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var dbfile = _context.PdfFiles.FirstOrDefault(x => x.Id == id);

            if(dbfile == null)
            {
                TempData["Error"] = " Pdf Dosyası bulunamadı.";
                return RedirectToAction("List", "PdfViewer");
            }

            _imageService.DeletePdfAndCoverImage(dbfile.SeoName);

            _context.Remove(dbfile);
            _context.SaveChanges();

            TempData["Success"] = " Pdf Dosyası silindi.";
            return RedirectToAction("List", "PdfViewer");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddPdfFileViewModel();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddPdfFileViewModel postFileEntity, IFormFile PdfFile)
        {
            var addEntity = _mapper.Map<PdfFile>(postFileEntity);
            addEntity.SeoName = UrlExtension.FriendlyUrl(addEntity.Name);
            addEntity.FileUrl = "";

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.PdfFiles.AddAsync(addEntity);
                    await _context.SaveChangesAsync();

                    if (PdfFile != null && PdfFile.Length > 0)
                    {
                        if (string.IsNullOrEmpty(addEntity.SeoName))
                            addEntity.SeoName = addEntity.Id.ToString();

                        var filePath = await _imageService.SaveFileAsync(PdfFile, addEntity.SeoName);

                        addEntity.FileUrl = filePath;
                        string imageFilePath = await _imageService.SavePdfFirstPageAsImageAsync(PdfFile, addEntity.SeoName);
                        if(imageFilePath != null)
                        {
                            addEntity.CoverImageUrl = imageFilePath;
                        }
                        _context.PdfFiles.Update(addEntity);
                        await _context.SaveChangesAsync();

                        
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();


                    TempData["Error"] = $"Pdf yükleme veya Kayıt işleminde hata oluştu \n {ex.Message}";
                    return RedirectToAction("List", "PdfViewer"); ;
                }
            }

            TempData["Success"] = " Pdf Dosyası yüklendi";
            return RedirectToAction("List", "PdfViewer");
        }


    }
}
