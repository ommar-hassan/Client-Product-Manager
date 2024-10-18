namespace ClientProductManager.Services
{
    public interface IClientService
    {
        Task<(IEnumerable<ClientViewModel>, int)> GetClientsAsync(int pageNumber, int pageSize);
        Task<ClientViewModel> GetClientAsync(Guid id);
        Task<ClientViewModel> GetClientWithProductsAsync(Guid id);
        Task<bool> AddClientAsync(ClientViewModel client);
        Task<bool> UpdateClientAsync(ClientViewModel client);
        Task<bool> DeleteClientAsync(Guid id);
        Task<bool> IsValidCodeAsync(string code);
    }
}
