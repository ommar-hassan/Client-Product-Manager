using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientProductManager.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly IClientService _clientService;

        public CreateModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        [BindProperty]
        public ClientViewModel Client { get; set; } = default!;

        public IEnumerable<SelectListItem> ClientClassList { get; set; } = default!;
        public IEnumerable<SelectListItem> ClientStateList { get; set; } = default!;

        public IActionResult OnGet()
        {
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

            if (!await _clientService.IsValidCodeAsync(Client.Code))
            {
                ModelState.AddModelError("Client.Code", "A client with this code already exists.");
                PopulateDropdowns();
                return Page();
            }

            var success = await _clientService.AddClientAsync(Client);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the client.");
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
