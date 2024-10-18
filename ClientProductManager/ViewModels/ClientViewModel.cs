namespace ClientProductManager.ViewModels
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Client name is required.")]
        [StringLength(50, ErrorMessage = "Client name cannot exceed 50 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Client code is required.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Client code must be exactly 9 digits.")]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Client class is required.")]
        [EnumDataType(typeof(ClientClass))]
        [Display(Name = "Class")]
        public ClientClass ClientClass { get; set; }

        [Required(ErrorMessage = "Client state is required.")]
        [EnumDataType(typeof(ClientState))]
        [Display(Name = "State")]
        public ClientState ClientState { get; set; }
    }
}
