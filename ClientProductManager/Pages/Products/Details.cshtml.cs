using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.ViewModels;
using ClientProductManager.Services;

namespace ClientProductManager.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;

        public DetailsModel(IProductService productService)
        {
            _productService = productService;
        }

        public ProductViewModel Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var productviewmodel = await _productService.GetProductAsync(id);

            if (productviewmodel == null)
            {
                return NotFound();
            }

            Product = productviewmodel;
            return Page();
        }
    }
}
