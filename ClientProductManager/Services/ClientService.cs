using ClientProductManager.Models;
using ClientProductManager.Repositories;
using ClientProductManager.ViewModels;

namespace ClientProductManager.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<bool> AddClientAsync(ClientViewModel clientViewModel)
        {
            ArgumentNullException.ThrowIfNull(clientViewModel);

            if (!await _clientRepository.IsValidCodeAsync(clientViewModel.Code)) // code is unique
                throw new ArgumentException("A client with this code already exists.");


            var client = new Client
            {
                Id = Guid.NewGuid(),
                Code = clientViewModel.Code,
                Name = clientViewModel.Name,
                ClientClass = clientViewModel.ClientClass,
                ClientState = clientViewModel.ClientState
            };

            await _clientRepository.AddClientAsync(client);
            return true;
        }

        public async Task<bool> UpdateClientAsync(ClientViewModel client)
        {
            ArgumentNullException.ThrowIfNull(client);

            var existingClient = await _clientRepository.GetClientByIdAsync(client.Id)
                ?? throw new ArgumentException("Client not found.");

            if (existingClient.Code != client.Code)
                throw new ArgumentException("Client Code cannot be changed.");

            existingClient.Name = client.Name;
            existingClient.ClientClass = client.ClientClass;
            existingClient.ClientState = client.ClientState;

            await _clientRepository.UpdateClientAsync(existingClient);
            return true;
        }

        public async Task<bool> DeleteClientAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id),"Invalid client ID");

            var client = await _clientRepository.GetClientWithProductsAsync(id)
                ?? throw new ArgumentException("Client not found.");

            if (client.ClientProducts != null && client.ClientProducts.Any())
                throw new InvalidOperationException("Client cannot be deleted because it has related products.");


            await _clientRepository.DeleteClientAsync(id);
            return true;
        }

        public async Task<ClientViewModel> GetClientAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Invalid client ID");

            var client = await _clientRepository.GetClientByIdAsync(id)
                ?? throw new ArgumentException("Client not found.");

            return new ClientViewModel
            {
                Id = client.Id,
                Name = client.Name,
                Code = client.Code,
                ClientClass = client.ClientClass,
                ClientState = client.ClientState
            };
        }

        public async Task<IEnumerable<ClientViewModel>> GetClientsAsync(int pageNumber = 1, int pageSize = 5)
        {
            var clients = await _clientRepository.GetClientsAsync(pageNumber, pageSize);

            return clients.Select(c => new ClientViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
                ClientClass = c.ClientClass,
                ClientState = c.ClientState
            });
        }

        public async Task<ClientViewModel> GetClientWithProductsAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Invalid client ID");

            var client = await _clientRepository.GetClientWithProductsAsync(id)
                ?? throw new ArgumentException("Client not found.");

            return new ClientViewModel
            {
                Id = client.Id,
                Name = client.Name,
                Code = client.Code,
                ClientClass = client.ClientClass,
                ClientState = client.ClientState,
                // Products
            };
        }
    }
}
