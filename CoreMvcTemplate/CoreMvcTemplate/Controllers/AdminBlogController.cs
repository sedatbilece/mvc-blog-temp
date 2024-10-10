using AutoMapper;
using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Entities.Enums;
using CoreMvcTemplate.Helpers;
using CoreMvcTemplate.Models.Blog;
using CoreMvcTemplate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Controllers
{
    [Authorize]
    public class AdminBlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;

        public AdminBlogController(AppDbContext context, ImageService imageService, IMapper mapper)
        {
            _context = context;
            _imageService = imageService;
            _mapper = mapper;
        }

        public IActionResult Index(PageType type)
        {
            var data = _context.Blogs.Where(x => x.PageType == type).OrderBy(x=>x.DisplayOrder).ThenBy(x => x.CreatedAt).ToList();
            TempData["PageType"] = EnumHelper.GetDisplayName(type);
            return View(data);
        }


        public IActionResult Detail(string id)
        {
            var data = _context.Blogs.FirstOrDefault(x => x.Id == id);
            var model = _mapper.Map<EditBlogViewModel>(data);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Detail(EditBlogViewModel blog, IFormFile BackgroundImageFile)
        {


            var existBlog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == blog.Id);

            string backImageUrl = "";
            if (BackgroundImageFile != null)
            {
                backImageUrl = await _imageService.SaveImageAsync(BackgroundImageFile, existBlog.HeadImageUrl);
            }
                

            if (!string.IsNullOrEmpty(backImageUrl))
                blog.HeadImageUrl = backImageUrl;
            else
                blog.HeadImageUrl = existBlog.HeadImageUrl;

            if (existBlog != null)
            {
                _mapper.Map<EditBlogViewModel, Blog>(blog, existBlog);
                existBlog.SeoTitle = UrlExtension.FriendlyUrl(existBlog.SeoTitle);

                _context.Blogs.Update(existBlog);
                await _context.SaveChangesAsync();
                TempData["Success"] = " Blog Güncellendi";
                return RedirectToAction("Index", "AdminBlog",new { type =existBlog.PageType});
            }
            else
            {
                TempData["Danger"] = " Blog Güncellenemedi !";
                return RedirectToAction("Index", "AdminBlog", new { type = existBlog.PageType });
            }

        }

        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogViewModel blog, IFormFile BackgroundImageFile)
        {
            string backImageUrl = "";
            if (BackgroundImageFile != null)
            {
                backImageUrl = await _imageService.SaveImageAsync(BackgroundImageFile, blog.HeadImageUrl);
            }

            if (!string.IsNullOrEmpty(backImageUrl))
                blog.HeadImageUrl = backImageUrl;

            var addBlog = _mapper.Map<Blog>(blog);
            addBlog.SeoTitle = UrlExtension.FriendlyUrl(addBlog.SeoTitle);
            await _context.Blogs.AddAsync(addBlog);
            await _context.SaveChangesAsync();

            TempData["Success"] = " Blog Eklendi";
            return RedirectToAction("Index", "AdminBlog", new { type = blog.PageType });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                TempData["Danger"] = " Blog Bulunamadı !";
                return RedirectToAction("Index", "AdminBlog");
            }

            _imageService.DeleteItemsImage(blog.HeadImageUrl);

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            TempData["Success"] = " Blog Silindi";
            return RedirectToAction("Index", "AdminBlog");
        }



    }
}
