using System;

namespace ShoppingCartService.Models
{    
    public class ShoppingCartItem
    {
        public ShoppingCartItem() { }

        public ShoppingCartItem(ShoppingCartItemDto itemDto)
        {
            CatalogItemId = itemDto.CatalogItemId;
            Amount = itemDto.Amount;
            UserId = itemDto.UserId;
        }

        public Guid ShoppingCartItemId { get; set; }
        public Guid CatalogItemId { get; set; }
        public int Amount { get; set; }
        public string UserId { get; set; }
    }
}
