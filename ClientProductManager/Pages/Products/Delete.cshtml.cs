using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Services;

namespace ClientProductManager.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;

        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductViewModel Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Product = await _productService.GetProductAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            bool success = await _productService.DeleteProductAsync(id);

            if (!success) {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the product.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
