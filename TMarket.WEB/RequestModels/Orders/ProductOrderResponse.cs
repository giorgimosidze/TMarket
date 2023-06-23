﻿namespace TMarket.WEB.RequestModels.Orders
{
    public class ProductOrderResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice
        {
            get => ProductPrice * Quantity;
        }
    }
}
