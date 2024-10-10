using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Models.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreMvcTemplate.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class StatisticsController : ControllerBase
	{
        private readonly AppDbContext _context;

        public StatisticsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<MainModel> Get()
        {
            
            
            var model = new MainModel();


            model.ActivePassiveBlogKeys.Add("Aktif Yazılar");
            model.ActivePassiveBlogValues.Add(_context.Blogs.Where(x => x.isActive).Count());
            model.ActivePassiveBlogKeys.Add("Pasif Yazılar");
            model.ActivePassiveBlogValues.Add(_context.Blogs.Where(x => !x.isActive).Count());


            model.BlogCorpTypeKeys.Add("Kurumsal Yazılar");
            model.BlogCorpTypeValues.Add(_context.Blogs.Where(x => x.PageType == Entities.Enums.PageType.Corporate).Count());
            model.BlogCorpTypeKeys.Add("Blog Yazıları");
            model.BlogCorpTypeValues.Add(_context.Blogs.Where(x => x.PageType == Entities.Enums.PageType.Blog).Count());

            model.OtherPageInfoKeys.Add("Mağazalar");
            model.OtherPageInfoValues.Add(_context.MarketPlaces.Count());

            model.OtherPageInfoKeys.Add("Ürünler");
            model.OtherPageInfoValues.Add(_context.Products.Count());

            model.OtherPageInfoKeys.Add("Dosyalar");
            model.OtherPageInfoValues.Add(_context.PdfFiles.Count());

            return Ok(model);
        }
    }
}
