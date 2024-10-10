using AutoMapper;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Entities.Enums;
using CoreMvcTemplate.Models.Blog;
using CoreMvcTemplate.Models.Home;
using CoreMvcTemplate.Models.Layout;
using CoreMvcTemplate.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.ViewComponents.Layout
{
    public class _LayoutHeaderComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public _LayoutHeaderComponent(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var model = new HeaderViewModel();
            var blogs = _dbContext.Blogs.AsNoTracking().Where(x => x.isActive && x.PageType == PageType.Blog).OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).Take(10).ToList();
            model.Blogs = _mapper.Map<List<BlogPropViewModel>>(blogs);

            var corporate = _dbContext.Blogs.AsNoTracking().Where(x => x.isActive && x.PageType == PageType.Corporate).OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).Take(10).ToList();
            model.Corporates = _mapper.Map<List<BlogPropViewModel>>(corporate);

            var products = _dbContext.Products.AsNoTracking().Where(x => x.isActive ).OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).Take(10).ToList();
            model.Products = _mapper.Map<List<ProductPropViewModel>>(products);

            model.LandingPage = _dbContext.LandingPages.AsNoTracking().FirstOrDefault();

            return View(model);

        }
    }
}
