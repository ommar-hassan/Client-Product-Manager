using System.ComponentModel.DataAnnotations;

namespace ClientProductManager.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(50, ErrorMessage = "Product name cannot exceed 50 characters.")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(150, ErrorMessage = "Product description cannot exceed 150 characters.")]
        [Display(Name = "Product Description")]
        public string Description { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
