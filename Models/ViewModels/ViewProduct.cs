using StockControlAPI.Models.Entities;

namespace StockControlAPI.Models.ViewModels
{
    public class ViewProduct
    {
       // public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public string? WarehouseName { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
