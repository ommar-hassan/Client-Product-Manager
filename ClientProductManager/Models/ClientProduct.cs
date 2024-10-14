using System.ComponentModel.DataAnnotations;

namespace ClientProductManager.Models
{
    public class ClientProduct
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required, StringLength(255)]
        public string License { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
