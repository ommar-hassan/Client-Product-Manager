using System.ComponentModel.DataAnnotations;

namespace ClientProductManager.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(150)]
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ClientProduct> ClientProducts { get; set; }
    }
}
