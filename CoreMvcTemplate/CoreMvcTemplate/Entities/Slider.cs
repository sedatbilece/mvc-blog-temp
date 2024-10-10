using CoreMvcTemplate.Entities.Data;

namespace CoreMvcTemplate.Entities
{
    public class Slider : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Title1 { get; set; }
        public string? Title2 { get; set; }
        public bool isActive { get; set; } = true;
        public int DisplayOrder { get; set; }
    }
}
