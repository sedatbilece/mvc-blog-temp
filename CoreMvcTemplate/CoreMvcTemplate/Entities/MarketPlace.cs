using CoreMvcTemplate.Entities.Data;

namespace CoreMvcTemplate.Entities
{
    public class MarketPlace : BaseEntity
    {
        public string  Name { get; set; }
        public string?  Title { get; set; }
        public string  RedirectUrl { get; set; }
        public string?  ImageUrl { get; set; }
        public int DisplayOrder { get; set; }

    }
}
