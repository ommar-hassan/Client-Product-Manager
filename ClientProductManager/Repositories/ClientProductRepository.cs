using ClientProductManager.Data;
using ClientProductManager.Models;
using ClientProductManager.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ClientProductManager.Repositories
{
    public class ClientProductRepository : IClientProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddClientProductAsync(ClientProduct clientProduct)
        {
            await _context.ClientProducts.AddAsync(clientProduct);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateClientProductAsync(ClientProduct clientProduct)
        {
            _context.ClientProducts.Update(clientProduct);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteClientProductAsync(Guid id)
        {
            var clientProduct = await GetClientProductAsync(id);
            if (clientProduct != null)
            {
                _context.ClientProducts.Remove(clientProduct);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<ClientProduct> GetClientProductAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Invalid ID.");

            return await _context.ClientProducts
                .AsNoTracking()
                .Include(cp => cp.Product)
                .FirstOrDefaultAsync(p => p.Id == id) ??
                throw new ArgumentException("Client product not found");
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _context.Products          
                .Where(p => p.IsActive)
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClientProduct>> GetClientProductsAsync(Guid clientId)
        {
            return await _context.ClientProducts
                .Include(cp => cp.Product)
                .Where(cp => cp.ClientId == clientId)
                .AsNoTracking()
                .OrderBy(cp => cp.Product.Name)
                .ToListAsync();
        }
    }
}
