using ClientProductManager.ViewModels;

namespace ClientProductManager.Services
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(ProductViewModel product);
        Task<bool> UpdateProductAsync(ProductViewModel product);
        Task<bool> DeleteProductAsync(Guid id);
        Task<IEnumerable<ProductViewModel>> GetProductsAsync();
        Task<ProductViewModel> GetProductAsync(Guid id);
    }
}
