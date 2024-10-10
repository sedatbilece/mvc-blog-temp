using CoreMvcTemplate.Entities.Enums;

namespace CoreMvcTemplate.Models.Blog
{
    public class BlogPropViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SeoTitle { get; set; }

        public string HeadImageUrl { get; set; }

        public PageType PageType { get; set; }
        public int DisplayOrder { get; set; }

    }
}
