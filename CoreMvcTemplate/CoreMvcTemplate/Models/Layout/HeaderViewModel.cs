using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Models.Blog;
using CoreMvcTemplate.Models.Products;

namespace CoreMvcTemplate.Models.Layout
{
    public class HeaderViewModel
    {
        public List<BlogPropViewModel> Blogs { get; set; }
        public List<BlogPropViewModel> Corporates { get; set; }
        public List<ProductPropViewModel> Products { get; set; }
        public LandingPage LandingPage { get; set; }
    }
}
