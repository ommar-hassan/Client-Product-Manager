using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Services;

namespace ClientProductManager.Pages.ClientProducts
{
    public class DetailsModel : PageModel
    {
        private readonly IClientProductService _clientProductService;

        public DetailsModel(IClientProductService clientProductService)
        {
            _clientProductService = clientProductService;
        }

        public ClientProductViewModel ClientProductViewModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var clientproductviewmodel = await _clientProductService.GetClientProductAsync(id);
            if (clientproductviewmodel == null)
            {
                return NotFound();
            }
            ClientProductViewModel = clientproductviewmodel;
            return Page();
        }
    }
}
