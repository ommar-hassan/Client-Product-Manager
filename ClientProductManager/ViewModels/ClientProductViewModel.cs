using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace ClientProductManager.ViewModels
{
    public class ClientProductViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Product")]
        public Guid ProductId { get; set; }
        public Guid ClientId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "License is required.")]
        [StringLength(255, ErrorMessage = "License cannot exceed 255 characters.")]
        public string License { get; set; }

        [BindNever]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
    }
}
