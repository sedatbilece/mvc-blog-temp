namespace CoreMvcTemplate.Models.Sliders
{
    public class EditSliderViewModel
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title1 { get; set; }
        public string? Title2 { get; set; }
        public bool isActive { get; set; } = true;
        public int DisplayOrder { get; set; }
    }
}
