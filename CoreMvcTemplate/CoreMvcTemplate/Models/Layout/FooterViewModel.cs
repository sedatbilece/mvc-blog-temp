using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Models.Blog;

namespace CoreMvcTemplate.Models.Layout
{
    public class FooterViewModel
    {
        public List<BlogPropViewModel> Corporates { get; set; }
        public LandingPage LandingPage { get; set; }
    }
}
