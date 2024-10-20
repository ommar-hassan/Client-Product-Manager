﻿using ClientProductManager.Data;
using Microsoft.EntityFrameworkCore;

namespace ClientProductManager.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.ClientProducts)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new ArgumentException("Product not found.");

            if (product.ClientProducts != null && product.ClientProducts.Count != 0)
            {
                throw new InvalidOperationException("Cannot delete the product as it is associated with client products.");
            }
            
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id) ?? throw new ArgumentException("Product not found");
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize)
        {
            return await _context.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync() // for pagination
        {
            return await _context.Products.CountAsync();
        }
    }
}
