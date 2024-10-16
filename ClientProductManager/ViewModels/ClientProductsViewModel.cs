using ClientProductManager.Models;
using System.ComponentModel.DataAnnotations;

namespace ClientProductManager.ViewModels
{
    public class ClientProductsViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "License is required.")]
        [StringLength(255, ErrorMessage = "License cannot exceed 255 characters.")]
        public string License { get; set; }
        public Guid ProductId { get; set; }
    }
}
