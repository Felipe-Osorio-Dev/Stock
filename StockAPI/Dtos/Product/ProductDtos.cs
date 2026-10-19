namespace StockAPI.Dtos.Product
{
    public record class CreateProductDto(string Name, string Ean, int Amount, DateOnly Validate);

    public record class UpdateProductDto(string Name, string Ean, int Amount, DateOnly Validate);

    public record class ProductDto(long Id, string Name, string Ean, int Amount, bool IsActive, DateOnly CreatedAt, DateOnly Validate);
}
