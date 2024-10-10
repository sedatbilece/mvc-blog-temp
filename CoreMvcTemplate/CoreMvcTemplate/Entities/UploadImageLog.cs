using CoreMvcTemplate.Entities.Data;

namespace CoreMvcTemplate.Entities
{
    public class UploadImageLog : BaseEntity
    {

        public string RequestUrl { get; set; }
        public string ImagePath { get; set; }
    }
}
