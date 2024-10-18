namespace ClientProductManager.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClientsAsync(int pageNumber, int pageSize);
        Task<Client> GetClientByIdAsync(Guid id);
        Task<bool> IsValidCodeAsync(string code);
        Task<Client> GetClientWithProductsAsync(Guid clientId);
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Guid id);
        Task<int> CountAsync();
    }
}
