using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using StockAPI.Common.Result;
using StockAPI.Dtos.Product;
using StockAPI.Entities;
using StockAPI.Repositorys.Product;

namespace StockAPI.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly int maxPageSize = 100;

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProductDto>> CreateProductAsync(CreateProductDto dto, CancellationToken ct = default)
        {
            var product = _mapper.Map<ProductEntity>(dto);

            try
            {
                await _productRepository.SaveProductAsync(product, ct);

            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                return EanConflict<ProductDto>();
            }

            return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
        }

        public async Task<Result> DeleteProductAsync(int productId, CancellationToken ct = default)
        {
            var product = await _productRepository.GetProductByIdAsync(productId, ct);

            if (product is null)
            {
                return Result.Failure(ResultStatus.NotFound, "Produto não encontrado.");
            }

            await _productRepository.DeleteProductAsync(product, ct);

            return Result.Success();
        }

        public async Task<Result<List<ProductDto>>> GetAllProductsAsync(int pageNumber, int pageSize, CancellationToken ct = default)
        {
            if (pageNumber < 1 || pageSize < 1 || pageSize > maxPageSize)
            {
                return Result<List<ProductDto>>.Failure(ResultStatus.Validation, $"Parâmetros de paginação inválidos. pageNumber deve ser >= 1 e pageSize deve ser entre 1 e {maxPageSize}.");
            }

            var products = await _productRepository.GetAllProductsAsync(pageNumber, pageSize, ct);
            return Result<List<ProductDto>>.Success((_mapper.Map<List<ProductDto>>(products)));
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int productId, CancellationToken ct = default)
        {
            var product = await _productRepository.GetProductByIdAsync(productId, ct);
            return product is null
                ? NotFound<ProductDto>()
                : Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
        }

        public async Task<Result<ProductDto>> UpdateProductAsync(int productId, UpdateProductDto dto, CancellationToken ct = default)
        {
            var product = await _productRepository.GetProductByIdAsync(productId, ct);

            if (product is null)
            {
                return Result<ProductDto>.Failure(ResultStatus.NotFound, "Produto não encontrado.");
            }

            _mapper.Map(dto, product);

            try
            {
                await _productRepository.UpdateProductAsync(product, ct);

            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                return EanConflict<ProductDto>();
            }

            return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
        }

        private static bool IsUniqueConstraintViolation(DbUpdateException ex)
            => ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation };

        private static Result<T> EanConflict<T>()
            => Result<T>.Failure(ResultStatus.Conflict, "Ja Existe um produto com o mesmo EAN.");

        private static Result<T> NotFound<T>()
            => Result<T>.Failure(ResultStatus.NotFound, "Produto não encontrado.");
    }
}
