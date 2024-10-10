using AutoMapper;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Entities.Enums;
using CoreMvcTemplate.Models.Blog;
using CoreMvcTemplate.Models.Layout;
using CoreMvcTemplate.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = new HeaderViewModel();
            var products = _context.Products.AsNoTracking().Where(x => x.isActive).OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).Take(10).ToList();
            model.Products = _mapper.Map<List<ProductPropViewModel>>(products);

            return View(model);
        }

        public IActionResult Detail(string title)
        {
            var data = _context.Products.AsNoTracking().Where(x => x.isActive).FirstOrDefault(x => x.SeoTitle == title);
            TempData["Title"] = data?.Title;
            if (data == null)
                return NotFound();

            return View(data);
        }
    }

}

