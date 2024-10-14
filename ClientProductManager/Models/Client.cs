using System.ComponentModel.DataAnnotations;

namespace ClientProductManager.Models
{
    public class Client
    {
        public Guid Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(9), RegularExpression(@"^\d{9}$")]
        public string Code { get; set; }
        public ClientClass ClientClass { get; set; }
        public ClientState ClientState { get; set; }

        public ICollection<ClientProduct> ClientProducts { get; set; }
    }
}
