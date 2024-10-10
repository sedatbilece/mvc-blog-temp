namespace CoreMvcTemplate.Models.PdfFile
{
    public class AddPdfFileViewModel
    {
        public string Name { get; set; }
        public string? SeoName { get; set; }
        public bool isActive { get; set; } = true;
        public int DisplayOrder { get; set; }
        public string FileUrl { get; set; }
    }
}
