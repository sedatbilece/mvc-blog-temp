using AutoMapper;
using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Models.LandingPages;
using CoreMvcTemplate.Models.MarketPlaceModels;
using CoreMvcTemplate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
        private readonly AppDbContext _context;
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;

        public AdminController(IMapper mapper, ImageService imageService, AppDbContext context)
        {
            _mapper = mapper;
            _imageService = imageService;
            _context = context;
        }

        public IActionResult Index()
		{
			TempData["User"] = User.Identity.Name;
			return View();
		}



		public IActionResult EditSiteInfo()
		{
            var data = _context.LandingPages.FirstOrDefault();
            var model = _mapper.Map<EditLandingPageViewModel>(data);
            return View(model);
		}


        [HttpPost]
        public async Task<IActionResult> EditSiteInfo(EditLandingPageViewModel landingPage, IFormFile LogoImageFile,IFormFile FaviconImageFile)
        {

            var existLandingPage = await _context.LandingPages.FirstOrDefaultAsync(x => x.Id == landingPage.Id);

            string ImageUrl = "";
            if (LogoImageFile != null)
                ImageUrl = await _imageService.SaveSiteImageAsync(LogoImageFile, existLandingPage.LogoUrl,"logo");

            if (!string.IsNullOrEmpty(ImageUrl))
                landingPage.LogoUrl = ImageUrl;
            else
                landingPage.LogoUrl = existLandingPage.LogoUrl;

            string ImageUrl2 = "";
            if (FaviconImageFile != null)
                ImageUrl2 = await _imageService.SaveSiteImageAsync(FaviconImageFile, existLandingPage.FaviconUrl,"favicon");

            if (!string.IsNullOrEmpty(ImageUrl2))
                landingPage.FaviconUrl = ImageUrl2;
            else
                landingPage.FaviconUrl = existLandingPage.FaviconUrl;

            if (existLandingPage != null)
            {
                _mapper.Map<EditLandingPageViewModel, LandingPage>(landingPage, existLandingPage);

                _context.LandingPages.Update(existLandingPage);
                await _context.SaveChangesAsync();
                TempData["Success"] = " Site Bilgileri Güncellendi";
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["Danger"] = " Site Bilgileri  Güncellenemedi !";
                return RedirectToAction("Index", "Admin");
            }
        }



    }
}
