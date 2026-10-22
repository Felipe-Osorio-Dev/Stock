using StockAPI.Common.Result;
using StockAPI.Dtos.Product;

namespace StockAPI.Services.Product
{
    public interface IProductService
    {
        Task<Result<List<ProductDto>>> GetAllProductsAsync(int pageNumber, int pageSize, CancellationToken ct = default);
        Task<Result<ProductDto>> GetProductByIdAsync(long productId, CancellationToken ct = default);
        Task<Result<ProductDto>> CreateProductAsync(CreateProductDto dto, CancellationToken ct = default);
        Task<Result<ProductDto>> UpdateProductAsync(long productId, UpdateProductDto dto, CancellationToken ct = default);
        Task<Result> DeleteProductAsync(long productId, CancellationToken ct = default);
    }
}
