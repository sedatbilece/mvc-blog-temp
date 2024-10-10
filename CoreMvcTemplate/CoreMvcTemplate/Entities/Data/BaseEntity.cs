using System.ComponentModel.DataAnnotations;

namespace CoreMvcTemplate.Entities.Data
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
