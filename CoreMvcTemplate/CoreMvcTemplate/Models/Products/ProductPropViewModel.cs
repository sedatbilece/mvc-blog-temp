using CoreMvcTemplate.Entities.Enums;

namespace CoreMvcTemplate.Models.Products
{
    public class ProductPropViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SeoTitle { get; set; }

        public string ImageUrl { get; set; }

        public int DisplayOrder { get; set; }
    }
}
