using CoreMvcTemplate.Entities;


namespace CoreMvcTemplate.Models.Home
{
    public class LandingPageViewModel
    {
        public List<Entities.Blog> Blogs { get; set; }
        public List<MarketPlace> MarketPlaces { get; set; }
        public List<Slider> Sliders { get; set; }
    }
}
