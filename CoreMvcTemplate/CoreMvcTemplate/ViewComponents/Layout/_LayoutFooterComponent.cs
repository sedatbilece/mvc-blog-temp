using AutoMapper;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Entities.Enums;
using CoreMvcTemplate.Models.Blog;
using CoreMvcTemplate.Models.Layout;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.ViewComponents.Layout
{
    public class _LayoutFooterComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public _LayoutFooterComponent(IMapper mapper, AppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var model  = new FooterViewModel();

            var corporate = _dbContext.Blogs.AsNoTracking().Where(x => x.isActive && x.PageType == PageType.Corporate).OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).Take(5).ToList();
            model.Corporates = _mapper.Map<List<BlogPropViewModel>>(corporate);

            model.LandingPage = _dbContext.LandingPages.FirstOrDefault();

            return View(model);
        }
    }
}
