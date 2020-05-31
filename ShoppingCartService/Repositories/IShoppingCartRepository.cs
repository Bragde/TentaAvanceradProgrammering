using ShoppingCartService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartService.Repositories
{
    public interface IShoppingCartRepository
    {
        List<ShoppingCartItem> GetShoppingCartItemsByShoppingCartId(Guid shoppingCartId);
        ShoppingCartItem GetItemFromShoppingCart(ShoppingCartItem shoppingCartItem);
        void AddItemToShoppingCart(ShoppingCartItem shoppingCartItem);
        Task<bool> Save();
    }
}
