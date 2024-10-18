namespace ClientProductManager.Services
{
    public class ClientProductService : IClientProductService
    {
        private IClientProductRepository _clientProductRepository;

        public ClientProductService(IClientProductRepository clientProductRepository)
        {
            this._clientProductRepository = clientProductRepository;
        }

        public async Task<bool> AddClientProductAsync(ClientProductViewModel clientProduct)
        {
            ArgumentNullException.ThrowIfNull(clientProduct);

            var newClientProduct = new ClientProduct
            {
                Id = Guid.NewGuid(),
                ClientId = clientProduct.ClientId,
                ProductId = clientProduct.ProductId,
                StartDate = clientProduct.StartDate,
                EndDate = clientProduct.EndDate,
                License = clientProduct.License
            };

            await _clientProductRepository.AddClientProductAsync(newClientProduct);

            return true;
        }

        public async Task<bool> UpdateClientProductAsync(ClientProductViewModel clientProduct)
        {
            ArgumentNullException.ThrowIfNull(clientProduct);

            var existingClientProduct = await _clientProductRepository.GetClientProductAsync(clientProduct.Id);

            ArgumentNullException.ThrowIfNull(existingClientProduct);

            existingClientProduct.ClientId = clientProduct.ClientId;
            existingClientProduct.ProductId = clientProduct.ProductId;
            existingClientProduct.StartDate = clientProduct.StartDate;
            existingClientProduct.EndDate = clientProduct.EndDate;
            existingClientProduct.License = clientProduct.License;

            await _clientProductRepository.UpdateClientProductAsync(existingClientProduct);

            return true;
        }

        public async Task<bool> DeleteClientProductAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Invalid client product ID.");

            await _clientProductRepository.DeleteClientProductAsync(id);

            return true;
        }

        public async Task<ClientProductViewModel> GetClientProductAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Invalid client product ID.");

            var clientProduct = await _clientProductRepository.GetClientProductAsync(id);

            return new ClientProductViewModel
            {
                Id = clientProduct.Id,
                ClientId = clientProduct.ClientId,
                ProductId = clientProduct.ProductId,
                StartDate = clientProduct.StartDate,
                EndDate = clientProduct.EndDate,
                License = clientProduct.License,
                ProductName = clientProduct.Product.Name
            };
        }

        public async Task<IEnumerable<ClientProductViewModel>> GetClientProductsByIdAsync(Guid clientId)
        {
            var clientProducts = await _clientProductRepository.GetClientProductsAsync(clientId);

            return clientProducts.Select(cp => new ClientProductViewModel
            {
                Id = cp.Id,
                ClientId = cp.ClientId,
                ProductId = cp.ProductId,
                StartDate = cp.StartDate,
                EndDate = cp.EndDate,
                License = cp.License,
                ProductName = cp.Product.Name
            });
        }

        public async Task<IEnumerable<ProductViewModel>> GetActiveProductsAsync()
        {
            var products = await _clientProductRepository.GetActiveProductsAsync();

            return products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                IsActive = p.IsActive
            });
        }
    }
}
