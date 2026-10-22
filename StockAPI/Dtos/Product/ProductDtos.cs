using System.ComponentModel.DataAnnotations;

namespace StockAPI.Dtos.Product
{
    public record class CreateProductDto(
        [property: Required, StringLength(50, MinimumLength = 1)] string Name,
        [property: Required, StringLength(13)] string Ean,
        [property: Required, Range(1, int.MaxValue)] int Amount,
        [property: Required] DateOnly Validate
    );

    public record class UpdateProductDto(
        [property: Required, StringLength(50, MinimumLength = 1)] string Name,
        [property: Required, StringLength(13)] string Ean,
        [property: Required, Range(1, int.MaxValue)] int Amount,
        [property: Required] DateOnly Validate
    );

    public record class ProductDto(long Id, string Name, string Ean, int Amount, bool IsActive, DateOnly CreatedAt, DateOnly Validate);

    public record class ApiErrorResponseDto(string Message, int StatusCode);
}
