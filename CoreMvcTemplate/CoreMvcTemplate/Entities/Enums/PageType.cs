using System.ComponentModel.DataAnnotations;

namespace CoreMvcTemplate.Entities.Enums
{
    public enum PageType
    {
        [Display(Name = "Blog")]
        Blog,
        [Display(Name = "Kurumsal")]
        Corporate,
    }
}
