namespace ClientProductManager.Repositories
{
    public interface IClientProductRepository
    {
        Task<bool> AddClientProductAsync(ClientProduct clientProduct);
        Task<bool> UpdateClientProductAsync(ClientProduct clientProduct);
        Task<bool> DeleteClientProductAsync(Guid id);
        Task<IEnumerable<ClientProduct>> GetClientProductsAsync(Guid clientId);
        Task<ClientProduct> GetClientProductAsync(Guid id);
        Task<IEnumerable<Product>> GetActiveProductsAsync();
    }
}
