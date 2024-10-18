using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Services;

namespace ClientProductManager.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public IEnumerable<ProductViewModel> Products { get;set; } = default!;
        public PaginationInfo Pagination { get; set; } = default!;

        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var (products, totalCount) = await _productService.GetProductsAsync(pageNumber, pageSize);
            Products = products;
            Pagination = new PaginationInfo(pageNumber, pageSize, totalCount);
        }
    }
}
