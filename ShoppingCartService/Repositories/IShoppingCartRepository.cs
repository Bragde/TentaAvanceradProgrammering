using ShoppingCartService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartService.Repositories
{
    public interface IShoppingCartRepository
    {
        List<ShoppingCartItem> GetShoppingCartByShoppingCartId(Guid shoppingCartId);
        ShoppingCartItem GetItemByShoppingCartItemId(Guid shopingCartItemId);
        void AddItemToShoppingCart(ShoppingCartItem shoppingCartItem);        
    }
}
