using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Data;

namespace Web.Models
{
    public class ShoppingCart
    {
        private readonly InMemoryShoppingCartItems _inMemoryShoppingCartItems;

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCart(InMemoryShoppingCartItems inMemoryShoppingCartItems)
        {
            _inMemoryShoppingCartItems = inMemoryShoppingCartItems;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<InMemoryShoppingCartItems>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context);
        }

        //public void AddToCart(CatalogItemDto product, int amount)
        //{
        //    var shoppingCartItem =
        //            _inMemoryShoppingCartItems.ShoppingCartItems.SingleOrDefault(
        //                s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

        //    if (shoppingCartItem == null)
        //    {
        //        shoppingCartItem = new ShoppingCartItem
        //        {
        //            ShoppingCartId = ShoppingCartId,
        //            Product = product,
        //            Amount = 1
        //        };

        //        _inMemoryShoppingCartItems.ShoppingCartItems.Add(shoppingCartItem);
        //    }
        //    else
        //    {
        //        shoppingCartItem.Amount++;
        //    }
        //}

        //public int RemoveFromCart(CatalogItemDto product)
        //{
        //    var shoppingCartItem =
        //            _inMemoryShoppingCartItems.ShoppingCartItems.SingleOrDefault(
        //                s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

        //    var localAmount = 0;

        //    if (shoppingCartItem != null)
        //    {
        //        if (shoppingCartItem.Amount > 1)
        //        {
        //            shoppingCartItem.Amount--;
        //            localAmount = shoppingCartItem.Amount;
        //        }
        //        else
        //        {
        //            _inMemoryShoppingCartItems.ShoppingCartItems.Remove(shoppingCartItem);
        //        }
        //    }

        //    return localAmount;
        //}

        //public List<ShoppingCartItem> GetShoppingCartItems()
        //{
        //    return ShoppingCartItems ??
        //           (ShoppingCartItems =
        //                _inMemoryShoppingCartItems.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
        //                .ToList());
        //}

        //public void ClearCart()
        //{
        //    var cartItems = _inMemoryShoppingCartItems
        //        .ShoppingCartItems
        //        .Where(cart => cart.ShoppingCartId == ShoppingCartId);

        //    _inMemoryShoppingCartItems.ShoppingCartItems
        //        .RemoveAll(item => cartItems.Contains(item));                
        //}

        //public decimal GetShoppingCartTotal()
        //{
        //    var total = _inMemoryShoppingCartItems.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
        //        .Select(c => c.Product.Price * c.Amount).Sum();
        //    return total;
        //}
    }
}
