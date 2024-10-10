using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Entities.Enums;

namespace CoreMvcTemplate.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string SeoTitle { get; set; }
        public string? Detail { get; set; }
        public string? Content { get; set; }
        public string? HeadImageUrl { get; set; }
        public bool isActive { get; set; } = true;
        public PageType PageType { get; set; }

        public int DisplayOrder { get; set; }
    }
}
