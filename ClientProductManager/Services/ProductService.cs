namespace ClientProductManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> AddProductAsync(ProductViewModel product)
        {
            ArgumentNullException.ThrowIfNull(product);

            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Description = product.Description,
                IsActive = product.IsActive
            };

            await _productRepository.AddProductAsync(newProduct);

            return true;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Invalid product ID");

            try
            {
                await _productRepository.DeleteProductAsync(id);
                return true;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Product cannot be deleted because it is associated with client products.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the product.", ex);
            }
        }

        public async Task<bool> UpdateProductAsync(ProductViewModel product)
        {
            ArgumentNullException.ThrowIfNull(product);

            var existingProduct = await _productRepository.GetProductAsync(product.Id);

            ArgumentNullException.ThrowIfNull(existingProduct);

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.IsActive = product.IsActive;

            await _productRepository.UpdateProductAsync(existingProduct);

            return true;
        }

        public async Task<ProductViewModel> GetProductAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Invalid product ID");

            var product = await _productRepository.GetProductAsync(id);

            ArgumentNullException.ThrowIfNull(product);

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                IsActive = product.IsActive
            };
        }

        public async Task<(IEnumerable<ProductViewModel>, int)> GetProductsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productRepository.GetProductsAsync(pageNumber, pageSize);
            var totalCount = await _productRepository.CountAsync();

            if (totalCount == 0)
                return ([], 0);

            var productViewModels = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                IsActive = p.IsActive
            });

            return (productViewModels, totalCount);
        }
    }
}
