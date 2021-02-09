﻿namespace Basket.API.Entities
{
    public class BasketCartItem
    {
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }

        public BasketCartItem(string name)
        {
            ProductName = name;
        }
        public BasketCartItem()
        {

        }
    }
}