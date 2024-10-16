using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.ViewModels;
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
            await _productService.DeleteProductAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
