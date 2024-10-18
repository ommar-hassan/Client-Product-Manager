using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientProductManager.Pages.ClientProducts
{
    public class EditModel : PageModel
    {
        private readonly IClientProductService _clientProductService;

        public EditModel(IClientProductService clientProductService)
        {
            _clientProductService = clientProductService;
        }

        [BindProperty]
        public ClientProductViewModel ClientProduct { get; set; } = default!;

        public IEnumerable<SelectListItem> ActiveProducts { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            ClientProduct = await _clientProductService.GetClientProductAsync(id);

            if (ClientProduct == null)
            {
                return NotFound();
            }

            var activeProducts = await _clientProductService.GetActiveProductsAsync();
            ActiveProducts = activeProducts.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"{p.Name} - {p.Description}"
            });

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
                var activeProducts = await _clientProductService.GetActiveProductsAsync();
                ActiveProducts = activeProducts.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.Name} - {p.Description}"
                });

                return Page();
            }

            var success = await _clientProductService.UpdateClientProductAsync(ClientProduct);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the product.");
                return Page();
            }

            return RedirectToPage("/Clients/Details", new { id = ClientProduct.ClientId });
        }
    }
}
