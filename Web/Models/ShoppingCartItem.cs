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
            ShoppingCartId = shoppingCartItemDto.ShoppingCartId;
        }

        public Guid ShoppingCartItemId { get; set; }
        public CatalogItemDto Product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
