using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Services;

namespace ClientProductManager.Pages.Clients
{
    public class DetailsModel : PageModel
    {
        private readonly IClientService _clientService;
        private readonly IClientProductService _clientProductService;

        public DetailsModel(IClientService clientService, IClientProductService clientProductService)
        {
            _clientService = clientService;
            _clientProductService = clientProductService;
        }

        public ClientViewModel Client { get; set; } = default!;
        public IEnumerable<ClientProductViewModel> ClientProducts { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var clientviewmodel = await _clientService.GetClientAsync(id);
            if (clientviewmodel == null)
            {
                return NotFound();
            }

            Client = clientviewmodel;
            ClientProducts = await _clientProductService.GetClientProductsByClientIdAsync(id);
            return Page();
        }
    }
}
