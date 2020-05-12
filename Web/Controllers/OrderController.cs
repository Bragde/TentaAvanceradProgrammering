using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ShoppingCart _shoppingCart;

        public OrderController(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        public IActionResult Purchase()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Purchase(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some products first");
            }

            if (ModelState.IsValid)
            {
                orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }
    }
}