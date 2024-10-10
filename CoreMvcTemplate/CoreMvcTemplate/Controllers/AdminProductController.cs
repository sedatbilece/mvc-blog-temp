using AutoMapper;
using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Entities.Enums;
using CoreMvcTemplate.Helpers;
using CoreMvcTemplate.Models.Blog;
using CoreMvcTemplate.Models.Products;
using CoreMvcTemplate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Controllers
{
    [Authorize]
    public class AdminProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;
        public AdminProductController(AppDbContext context, ImageService imageService, IMapper mapper)
        {
            _context = context;
            _imageService = imageService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var data = _context.Products.OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel prd, IFormFile ImageFile)
        {
            string backImageUrl = "";
            if (ImageFile != null)
            {
                backImageUrl = await _imageService.SaveImageAsync(ImageFile, prd.ImageUrl);
            }
                
            if (!string.IsNullOrEmpty(backImageUrl))
                prd.ImageUrl = backImageUrl;

            var addPrd = _mapper.Map<Product>(prd);
            addPrd.SeoTitle = UrlExtension.FriendlyUrl(addPrd.SeoTitle);
            await _context.Products.AddAsync(addPrd);
            await _context.SaveChangesAsync();

            TempData["Success"] = " Ürün Eklendi";
            return RedirectToAction("Index", "AdminProduct");
        }

        public IActionResult Detail(string id)
        {
            var data = _context.Products.FirstOrDefault(x => x.Id == id);
            var model = _mapper.Map<EditProductViewModel>(data);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(EditProductViewModel prd, IFormFile ImageFile)
        {
            var existPrd = await _context.Products.FirstOrDefaultAsync(x => x.Id == prd.Id);

            string backImageUrl = "";
            if (ImageFile != null)
            {
                backImageUrl = await _imageService.SaveImageAsync(ImageFile, existPrd.ImageUrl);
            }      

            if (!string.IsNullOrEmpty(backImageUrl))
                prd.ImageUrl = backImageUrl;
            else
                prd.ImageUrl = existPrd.ImageUrl;

            if (existPrd != null)
            {
                _mapper.Map<EditProductViewModel, Product>(prd, existPrd);
                existPrd.SeoTitle = UrlExtension.FriendlyUrl(existPrd.SeoTitle);

                _context.Products.Update(existPrd);
                await _context.SaveChangesAsync();
                TempData["Success"] = " Ürün Güncellendi";
                return RedirectToAction("Index", "AdminProduct");
            }
            else
            {
                TempData["Danger"] = " Ürün Güncellenemedi !";
                return RedirectToAction("Index", "AdminProduct");
            }
        }


        public async Task<IActionResult> Delete(string id)
        {
            var prd = await _context.Products.FindAsync(id);
            if (prd == null)
            {
                TempData["Danger"] = " Ürün Bulunamadı !";
                return RedirectToAction("Index", "AdminProduct");
            }

            _imageService.DeleteItemsImage(prd.ImageUrl);

            _context.Products.Remove(prd);
            await _context.SaveChangesAsync();

            TempData["Success"] = " Ürün Silindi";
            return RedirectToAction("Index", "AdminProduct");
        }



    }
}
