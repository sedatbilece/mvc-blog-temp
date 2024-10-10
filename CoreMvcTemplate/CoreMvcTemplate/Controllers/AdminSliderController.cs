using AutoMapper;
using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Entities.Enums;
using CoreMvcTemplate.Helpers;
using CoreMvcTemplate.Models.Products;
using CoreMvcTemplate.Models.Sliders;
using CoreMvcTemplate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Controllers
{
    [Authorize]
    public class AdminSliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;

        public AdminSliderController(IMapper mapper, ImageService imageService, AppDbContext context)
        {
            _mapper = mapper;
            _imageService = imageService;
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Sliders.OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddSliderViewModel slider, IFormFile ImageFile)
        {

            if(_context.Sliders.Where(x => x.isActive).Count() >= 3)
            {
                TempData["Error"] = " Slider Eklenemedi  : 3 adetten fazla eklenemez.";
                return RedirectToAction("Index", "AdminSlider");
            }

            string backImageUrl = "";
            if (ImageFile != null)
            {
                backImageUrl = await _imageService.SaveImageAsync(ImageFile, slider.ImageUrl);
            }
                

            if (!string.IsNullOrEmpty(backImageUrl))
                slider.ImageUrl = backImageUrl;

            var addSlider = _mapper.Map<Slider>(slider);

            await _context.Sliders.AddAsync(addSlider);
            await _context.SaveChangesAsync();

            TempData["Success"] = " Slider Eklendi";
            return RedirectToAction("Index", "AdminSlider");
        }


        public IActionResult Detail(string id)
        {
            var data = _context.Sliders.FirstOrDefault(x => x.Id == id);
            var model = _mapper.Map<EditSliderViewModel>(data);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(EditSliderViewModel slider, IFormFile ImageFile)
        {

            var existSlider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == slider.Id);

            if(existSlider.isActive ==false && slider.isActive == true)
            {
                if (_context.Sliders.Where(x => x.isActive).Count() >= 3)
                {
                    TempData["Error"] = " Slider Güncellenemedi  : 3 adetten fazla aktif slider olamaz.";
                    return RedirectToAction("Index", "AdminSlider");
                }
            }

            string backImageUrl = "";
            if (ImageFile != null)
            {
                backImageUrl = await _imageService.SaveImageAsync(ImageFile, existSlider.ImageUrl);
            }
                

            if (!string.IsNullOrEmpty(backImageUrl))
                slider.ImageUrl = backImageUrl;
            else
                slider.ImageUrl = existSlider.ImageUrl;

            if (existSlider != null)
            {
                _mapper.Map<EditSliderViewModel, Slider>(slider, existSlider);


                _context.Sliders.Update(existSlider);
                await _context.SaveChangesAsync();
                TempData["Success"] = " Slider Güncellendi";
                return RedirectToAction("Index", "AdminSlider");
            }
            else
            {
                TempData["Danger"] = " Slider Güncellenemedi !";
                return RedirectToAction("Index", "AdminSlider");
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                TempData["Danger"] = " Slider Bulunamadı !";
                return RedirectToAction("Index", "AdminSlider");
            }

            _imageService.DeleteItemsImage(slider.ImageUrl);

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

            TempData["Success"] = " Slider Silindi";
            return RedirectToAction("Index", "AdminSlider");
        }

    }
}
