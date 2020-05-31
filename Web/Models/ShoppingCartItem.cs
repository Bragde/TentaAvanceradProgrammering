﻿using System;

namespace Web.Models
{
    public class ShoppingCartItem
    {
        public Guid ShoppingCartItemId { get; set; }
        public CatalogItemDto Product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
