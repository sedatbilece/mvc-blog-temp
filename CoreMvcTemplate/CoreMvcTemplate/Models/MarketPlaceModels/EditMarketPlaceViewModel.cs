namespace CoreMvcTemplate.Models.MarketPlaceModels
{
    public class EditMarketPlaceViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string RedirectUrl { get; set; }
        public string ImageUrl { get; set; }
        public int DisplayOrder { get; set; }
    }
}
