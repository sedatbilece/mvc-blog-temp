using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Blogs.AsNoTracking().Where(x => x.isActive && x.PageType == Entities.Enums.PageType.Blog).OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedAt).ToList();
            foreach (var blog in data)
            {
                if (blog.Content.Length > 200)
                {
                    blog.Content = StringHelper.OnlyString(blog.Content?.Substring(0, 200));
                }
                else
                {
                    blog.Content = StringHelper.OnlyString(blog.Content?.Substring(0, blog.Content.Length - 1));
                }

            }
            return View(data);
        }

        public IActionResult Detail(string title)
        {
            var data = _context.Blogs.AsNoTracking().Where(x => x.isActive).FirstOrDefault(x => x.SeoTitle == title);
            TempData["Title"] = data?.Title;
            if (data == null)
                return NotFound();

            return View(data);
        }
    }
}
