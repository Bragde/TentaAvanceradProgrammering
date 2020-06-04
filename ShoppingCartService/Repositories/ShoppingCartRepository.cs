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

        public List<ShoppingCartItem> GetShoppingCartItemsByUserId(string userId)
        {
            if (String.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            var shoppingCartItems = _context.ShoppingCartItems
                .Where(x => x.UserId == userId)
                .ToList();

            return shoppingCartItems;
        }

        public ShoppingCartItem GetItemFromShoppingCart(ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem == null)
                throw new ArgumentNullException(nameof(shoppingCartItem));

            var item = _context.ShoppingCartItems
                .FirstOrDefault(x => 
                    x.CatalogItemId == shoppingCartItem.CatalogItemId
                    && x.UserId == shoppingCartItem.UserId);

            return item;
        }

        public void AddItemToShoppingCart(ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem == null)
                throw new ArgumentNullException(nameof(shoppingCartItem));

            _context.ShoppingCartItems.Add(shoppingCartItem);
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void DeleteShoppingCart(string userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var itemsToBeRemoved = GetShoppingCartItemsByUserId(userId);

            _context.RemoveRange(itemsToBeRemoved);
        }
    }
}
