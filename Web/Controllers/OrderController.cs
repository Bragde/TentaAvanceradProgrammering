using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ShoppingCart shoppingCart, 
            IOrderRepository orderRepository,
            UserManager<ApplicationUser> userManager)
        {
            _shoppingCart = shoppingCart;
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Purchase()
        {
            var orderViewModel = new OrderViewModel
            {
                User = await _userManager.GetUserAsync(User)
            };

            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some products first");
            }

            if (ModelState.IsValid)
            {
                //Add shoppingcart items to order
                foreach (var item in items)
                {
                    var orderDetail = new OrderDetail
                    {
                        Product = item.Product,
                        Amount = item.Amount
                    };
                    order.OrderDetails.Add(orderDetail);
                }

                var vm = new OrderViewModel
                {
                    Order = order,
                    User = await _userManager.GetUserAsync(User)
                };

                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return View("PurchaseComplete", vm);
            }
            return View(new OrderViewModel());
        }
    }
}