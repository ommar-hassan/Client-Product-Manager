using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Services;

namespace ClientProductManager.Pages.ClientProducts
{
    public class DeleteModel : PageModel
    {
        private readonly IClientProductService _clientProductService;

        public DeleteModel(IClientProductService clientProductService)
        {
            _clientProductService = clientProductService;
        }

        [BindProperty]
        public ClientProductViewModel ClientProductViewModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            ClientProductViewModel = await _clientProductService.GetClientProductAsync(id);

            if (ClientProductViewModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var success = await _clientProductService.DeleteClientProductAsync(id);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the client product.");
                return Page();
            }

            return RedirectToPage("/Clients/Details", new { id = ClientProductViewModel.ClientId });
        }
    }
}
