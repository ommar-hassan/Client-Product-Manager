namespace ClientProductManager.Services
{
    public interface IClientProductService
    {
        Task<bool> AddClientProductAsync(ClientProductViewModel clientProduct);
        Task<bool> UpdateClientProductAsync(ClientProductViewModel clientProduct);
        Task<bool> DeleteClientProductAsync(Guid id);
        Task<IEnumerable<ClientProductViewModel>> GetClientProductsByClientIdAsync(Guid clientId);
        Task<ClientProductViewModel> GetClientProductAsync(Guid id);
        Task<IEnumerable<ProductViewModel>> GetActiveProductsAsync();
    }
}
