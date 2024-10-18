using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientProductManager.Pages.Clients
{
    public class EditModel : PageModel
    {
        private readonly IClientService _clientService;

        public EditModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        [BindProperty]
        public ClientViewModel Client { get; set; } = default!;
        public IEnumerable<SelectListItem> ClientClassList { get; set; } = default!;
        public IEnumerable<SelectListItem> ClientStateList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var clientviewmodel =  await _clientService.GetClientAsync(id);
            if (clientviewmodel == null)
            {
                return NotFound();
            }

            Client = clientviewmodel;
            PopulateDropdowns();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return Page();
            }

            bool succcess = await _clientService.UpdateClientAsync(Client);

            if (!succcess)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the client.");
                PopulateDropdowns();
                return Page();
            }

            return RedirectToPage("./Index");
        }

        private void PopulateDropdowns()
        {
            ClientClassList = Enum.GetValues(typeof(ClientClass))
                .Cast<ClientClass>()
                .Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = c.ToString()
                });

            ClientStateList = Enum.GetValues(typeof(ClientState))
                .Cast<ClientState>()
                .Select(s => new SelectListItem
                {
                    Value = s.ToString(),
                    Text = s.ToString()
                });
        }
    }
}
