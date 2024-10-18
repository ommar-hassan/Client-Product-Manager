namespace ClientProductManager.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize);
        Task<Product> GetProductAsync(Guid id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
        Task<int> CountAsync();
    }
}
