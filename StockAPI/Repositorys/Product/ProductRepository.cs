using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Entities;

namespace StockAPI.Repositorys.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly  AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteProductAsync(ProductEntity product, CancellationToken ct = default)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(ct);
        }

        public Task<List<ProductEntity>> GetAllProductsAsync(int page, int pageSize, CancellationToken ct = default)
            => _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        public Task<ProductEntity?> GetProductByIdAsync(int id, CancellationToken ct = default)
            => _context.Products.FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task SaveProductAsync(ProductEntity product, CancellationToken ct = default)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateProductAsync(ProductEntity product, CancellationToken ct = default)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(ct);
        }
    }
}
