using ClientProductManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientProductManager.Pages.ClientProducts
{
    public class AssignProductsModel : PageModel
    {
        private readonly IClientProductService _clientProductService;
        private readonly IClientService _clientService;

        public AssignProductsModel(IClientProductService clientProductService, IClientService clientService)
        {
            _clientProductService = clientProductService;
            _clientService = clientService;
        }

        [BindProperty]
        public ClientProductViewModel ClientProduct { get; set; } = default!;

        public string ClientName { get; set; } = string.Empty;
        public IEnumerable<ProductViewModel> ActiveProducts { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(Guid clientId)
        {
            var client = await _clientService.GetClientAsync(clientId);
            if (client == null)
            {
                return NotFound();
            }

            ClientProduct = new ClientProductViewModel { 
                ClientId = clientId, 
                StartDate = DateTime.Now 
            };

            ClientName = client.Name;
            ActiveProducts = await _clientProductService.GetActiveProductsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("ClientProduct.ProductName");

            if (ClientProduct.StartDate < DateTime.Today)
            {
                ModelState.AddModelError("ClientProduct.StartDate", "Start date must be today or later.");
            }

            if (ClientProduct.EndDate.HasValue && ClientProduct.EndDate <= ClientProduct.StartDate)
            {
                ModelState.AddModelError("ClientProduct.EndDate", "End date must be later than the start date.");
            }

            if (!ModelState.IsValid)
            {
                ActiveProducts = await _clientProductService.GetActiveProductsAsync();
                return Page();
            }

            var success = await _clientProductService.AddClientProductAsync(ClientProduct);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while assigning the product.");
                ActiveProducts = await _clientProductService.GetActiveProductsAsync();
                return Page();
            }

            return RedirectToPage("/Clients/Details", new { id = ClientProduct.ClientId });
        }
    }

}
