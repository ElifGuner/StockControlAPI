namespace StockControlAPI.Models.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string WarehouseName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
