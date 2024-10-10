using CoreMvcTemplate.Entities.Data;

namespace CoreMvcTemplate.Entities
{
    public class LandingPage : BaseEntity
    {
        public string? FirmName { get; set; }
        public string? FirmTitle { get; set; }
        public string? FirmDesc { get; set; }
        public string? FaviconUrl { get; set; }
        public string? LogoUrl { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Mail1 { get; set; }
        public string? Mail2 { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? MapsUrl { get; set; }
        public string? Linkedin { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
    }
}
