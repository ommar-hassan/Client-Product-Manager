using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Services;

namespace ClientProductManager.Pages.Clients
{
    public class DeleteModel : PageModel
    {
        private readonly IClientService _clientService;

        public DeleteModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        [BindProperty]
        public ClientViewModel Client { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var clientviewmodel = await _clientService.GetClientAsync(id);

            if (clientviewmodel == null)
            {
                return NotFound();
            }

            Client = clientviewmodel;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            bool success = await _clientService.DeleteClientAsync(id);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the client.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
