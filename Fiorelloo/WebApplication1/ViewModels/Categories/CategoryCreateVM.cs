using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Categories
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "Bosh qoymaq olmaz!!!")]
        public string Name { get; set; }
    }
}
