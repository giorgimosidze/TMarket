using System;

namespace TMarket.WEB.RequestModels.Products
{
    public class ProductRespond
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime UsefulnessTerm { get; set; }
        public int AvailableCount { get; set; }
    }
}
