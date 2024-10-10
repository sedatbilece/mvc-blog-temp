using AutoMapper;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Models;
using CoreMvcTemplate.Models.Blog;
using CoreMvcTemplate.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CoreMvcTemplate.Entities.Enums;
using CoreMvcTemplate.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = new LandingPageViewModel();


            var blogs = _dbContext.Blogs.AsNoTracking().Where(x => x.isActive && x.PageType == PageType.Blog).OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).Take(4).ToList();
            foreach (var blog in blogs)
            {
                if (blog.Content.Length > 200)
                {
                    blog.Content = StringHelper.OnlyString(blog.Content?.Substring(0, 200));
                }
                else
                {
                    blog.Content = StringHelper.OnlyString(blog.Content?.Substring(0, blog.Content.Length));
                }
                
            }
            model.Blogs = blogs;

            model.MarketPlaces = _dbContext.MarketPlaces.AsNoTracking().OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).Take(6).ToList();

            model.Sliders = _dbContext.Sliders.AsNoTracking().Where(x => x.isActive ).OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).Take(1).ToList();


            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
