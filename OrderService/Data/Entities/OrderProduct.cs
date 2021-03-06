﻿using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data.Entities
{
    public class OrderProduct
    {
        public OrderProduct()
        {

        }

        public OrderProduct(OrderProductDto orderProduct)
        {
            Title = orderProduct.Product.Title;
            Price = orderProduct.Product.Price;
            Amount = orderProduct.Amount;
        }

        public Guid OrderProductId { get; set; }
        public Guid OrderId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
