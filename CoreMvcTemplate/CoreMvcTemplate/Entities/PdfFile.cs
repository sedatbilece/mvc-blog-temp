using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Entities.Enums;

namespace CoreMvcTemplate.Entities
{
    public class PdfFile : BaseEntity
    {
        public string  Name { get; set; }
        public string?  SeoName { get; set; }
        public bool isActive { get; set; } = true;
        public int DisplayOrder { get; set; }
        public string FileUrl { get; set; }
        public string? CoverImageUrl { get; set; }

        
    }
}
