using StockAPI.Entities;

namespace StockAPI.Repositorys.Product
{
    public interface IProductRepository
    {
        Task<ProductEntity?> GetProductByIdAsync(long id, CancellationToken ct = default);
        Task<List<ProductEntity>> GetAllProductsAsync(int page, int pageSize, CancellationToken ct = default);
        Task SaveProductAsync(ProductEntity product, CancellationToken ct = default);
        Task UpdateProductAsync(ProductEntity product, CancellationToken ct = default);
        Task DeleteProductAsync(ProductEntity product, CancellationToken ct = default);
    }
}
