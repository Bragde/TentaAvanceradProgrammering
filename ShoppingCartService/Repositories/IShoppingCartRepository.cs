using ShoppingCartService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartService.Repositories
{
    public interface IShoppingCartRepository
    {
        List<ShoppingCartItem> GetShoppingCartItemsByUserId(string userId);
        ShoppingCartItem GetItemFromShoppingCart(ShoppingCartItem shoppingCartItem);
        void AddItemToShoppingCart(ShoppingCartItem shoppingCartItem);
        void DeleteShoppingCart(string userId);
        Task<bool> Save();
    }
}
