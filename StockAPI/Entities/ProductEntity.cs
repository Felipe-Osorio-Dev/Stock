namespace StockAPI.Entities
{
    public class ProductEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Ean { get; set; } = string.Empty;
        public int Amount { get; set; }
        public bool IsActive { get; set; } = false;
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly Validate { get; set; }
    }
}
