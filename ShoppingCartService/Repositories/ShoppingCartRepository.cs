using ShoppingCartService.Data.Context;
using ShoppingCartService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartService.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShoppingCartDbContext _context;

        public ShoppingCartRepository(ShoppingCartDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<ShoppingCartItem> GetShoppingCartByShoppingCartId(Guid ShoppingCartId)
        {
            if (ShoppingCartId == Guid.Empty)
                throw new ArgumentNullException(nameof(ShoppingCartId));

            var shoppingCartItems = _context.ShoppingCartItems
                .Where(x => x.ShoppingCartId == ShoppingCartId)
                .ToList();

            return shoppingCartItems;
        }

        public ShoppingCartItem GetItemByShoppingCartItemId(Guid shoppingCartItemId)
        {
            if (shoppingCartItemId == Guid.Empty)
                throw new ArgumentNullException(nameof(shoppingCartItemId));

            var item = _context.ShoppingCartItems
                .FirstOrDefault(x => x.ShoppingCartItemId == shoppingCartItemId);

            return item;
        }

        public void AddItemToShoppingCart(ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem == null)
                throw new ArgumentNullException(nameof(shoppingCartItem));

            if (shoppingCartItem.ShoppingCartId == null)
                shoppingCartItem.ShoppingCartId = Guid.NewGuid();

            _context.ShoppingCartItems.Add(shoppingCartItem);
            _context.SaveChanges();
        }
    }
}
