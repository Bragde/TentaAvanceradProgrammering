using System;

namespace Web.Models
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem()
        {

        }
        public ShoppingCartItem(ShoppingCartItemDto shoppingCartItemDto)
        {
            ShoppingCartItemId = shoppingCartItemDto.ShoppingCartItemId;
            Amount = shoppingCartItemDto.Amount;
            UserId = shoppingCartItemDto.UserId;
        }

        public Guid ShoppingCartItemId { get; set; }
        public CatalogItemDto Product { get; set; }
        public int Amount { get; set; }
        public string UserId { get; set; }
    }
}
