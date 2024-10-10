using AutoMapper;
using CoreMvcTemplate.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Controllers
{
    public class StaticPagesController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public StaticPagesController(IMapper mapper, AppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public IActionResult Contact()
        {
            var landPage = _dbContext.LandingPages.AsNoTracking().FirstOrDefault();
            return View(landPage);
        }
    }
}
