using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.ViewModels;
using ClientProductManager.Services;

namespace ClientProductManager.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;

        public EditModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductViewModel Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var productviewmodel =  await _productService.GetProductAsync(id);
            if (productviewmodel == null)
            {
                return NotFound();
            }
            Product = productviewmodel;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _productService.UpdateProductAsync(Product);

            return RedirectToPage("./Index");
        }
    }
}
