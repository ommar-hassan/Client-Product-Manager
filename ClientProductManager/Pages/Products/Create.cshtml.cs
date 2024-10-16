using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.ViewModels;
using ClientProductManager.Services;

namespace ClientProductManager.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;

        public CreateModel(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProductViewModel Product { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _productService.AddProductAsync(Product);

            return RedirectToPage("./Index");
        }
    }
}
