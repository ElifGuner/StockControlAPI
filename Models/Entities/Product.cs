namespace StockControlAPI.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int WarehouseId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public Warehouse Warehouse { get; set; }

    }
}
