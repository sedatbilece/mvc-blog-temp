using AutoMapper;
using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Entities.Enums;
using CoreMvcTemplate.Helpers;
using CoreMvcTemplate.Models.Blog;
using CoreMvcTemplate.Models.MarketPlaceModels;
using CoreMvcTemplate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Controllers
{
    [Authorize]
    public class AdminMarketPlaceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;
        public AdminMarketPlaceController(AppDbContext context, ImageService imageService, IMapper mapper)
        {
            _context = context;
            _imageService = imageService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var model = new List<MarketPlace>();
            model = _context.MarketPlaces.OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).ToList();
            return View(model);
        }


        public IActionResult Add()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddMarketPlaceViewModel addMarketPlace, IFormFile BackgroundImageFile)
        {
            string backImageUrl = "";
            if (BackgroundImageFile != null)
            {
                backImageUrl = await _imageService.SaveImageAsync(BackgroundImageFile, addMarketPlace.ImageUrl);
            }
                

            if (!string.IsNullOrEmpty(backImageUrl))
                addMarketPlace.ImageUrl = backImageUrl;

            var mappedItem = _mapper.Map<MarketPlace>(addMarketPlace);
            await _context.MarketPlaces.AddAsync(mappedItem);
            await _context.SaveChangesAsync();

            TempData["Success"] = " Pazar Yeri Eklendi";
            return RedirectToAction("Index", "AdminMarketPlace");
        }


        public IActionResult Detail(string id)
        {
            var data = _context.MarketPlaces.FirstOrDefault(x => x.Id == id);
            var model = _mapper.Map<EditMarketPlaceViewModel>(data);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Detail(EditMarketPlaceViewModel marketPlace, IFormFile BackgroundImageFile)
        {

            var existMarketPlace = await _context.MarketPlaces.FirstOrDefaultAsync(x => x.Id == marketPlace.Id);

            string backImageUrl = "";
            if (BackgroundImageFile != null)
            {
                backImageUrl = await _imageService.SaveImageAsync(BackgroundImageFile, existMarketPlace.ImageUrl);
            }
               

            if (!string.IsNullOrEmpty(backImageUrl))
                marketPlace.ImageUrl = backImageUrl;
            else
                marketPlace.ImageUrl = existMarketPlace.ImageUrl;

            if (existMarketPlace != null)
            {
                _mapper.Map<EditMarketPlaceViewModel, MarketPlace>(marketPlace, existMarketPlace);

                _context.MarketPlaces.Update(existMarketPlace);
                await _context.SaveChangesAsync();
                TempData["Success"] = " Pazar Yeri Güncellendi";
                return RedirectToAction("Index", "AdminMarketPlace");
            }
            else
            {
                TempData["Danger"] = " Pazar Yeri Güncellenemedi !";
                return RedirectToAction("Index", "AdminMarketPlace");
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var marketPlace = await _context.MarketPlaces.FindAsync(id);
            if (marketPlace == null)
            {
                TempData["Danger"] = "  Pazar Yeri Bulunamadı !";
                return RedirectToAction("Index", "AdminMarketPlace");
            }

            _imageService.DeleteItemsImage(marketPlace.ImageUrl);

            _context.MarketPlaces.Remove(marketPlace);
            await _context.SaveChangesAsync();

            TempData["Success"] = "  Pazar Yeri Silindi";
            return RedirectToAction("Index", "AdminMarketPlace");
        }


    }
}
