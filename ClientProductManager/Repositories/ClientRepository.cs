using ClientProductManager.Data;
using Microsoft.EntityFrameworkCore;

namespace ClientProductManager.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddClientAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(Guid id)
        {
            var client = await GetClientByIdAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Client> GetClientByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return await _context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id) ?? throw new ArgumentException("Client not found");
        }

        public async Task<IEnumerable<Client>> GetClientsAsync(int pageNumber, int pageSize)
        {
            return await _context.Clients
                .AsNoTracking()
                .OrderBy(c => c.Code)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Client> GetClientWithProductsAsync(Guid id)
        {
            var client =  await _context.Clients
                .AsNoTracking()
                .Include(c => c.ClientProducts)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.Id == id) ?? throw new ArgumentException("Client not found");

            client.ClientProducts = client
                .ClientProducts
                .OrderBy(cp => cp.Product.Name)
                .ToList();

            return client;
        }

        public async Task<bool> IsValidCodeAsync(string code)
        {
            return !await _context.Clients
                .AsNoTracking()
                .AnyAsync(c => c.Code == code);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Clients.CountAsync();
        }
    }
}
