using AutoMapper;
using CoreMvcTemplate.Entities.Data;
using Microsoft.AspNetCore.Mvc;

namespace CoreMvcTemplate.ViewComponents.Layout
{
    public class _LayoutHeadComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public _LayoutHeadComponent(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IViewComponentResult Invoke()
        {
            var model = _dbContext.LandingPages.FirstOrDefault();
            return View(model);
        }
    }
}
