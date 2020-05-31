using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IHttpClientFactory _clientFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(ShoppingCart shoppingCart,
            IHttpClientFactory clientFactory,
            UserManager<ApplicationUser> userManager)
        {
            _shoppingCart = shoppingCart;
            _clientFactory = clientFactory;
            _userManager = userManager;
        }

        // SHOW SHOPPINGCART VIEW
        public async Task<IActionResult> Index()
        {
            // Get user shoppingcart id
            var user = await _userManager.GetUserAsync(User);
            var shoppingCartId = user.ShoppingCartId;

            // Get shoppingcart items by shoppingcart id
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:51045/shoppingcartservice/shoppingcarts/{shoppingCartId}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "AvcPgm.UI");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var shoppingCartItems = await JsonSerializer.DeserializeAsync<IEnumerable<ShoppingCartItemDto>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                // Create shoppingcart viewmodel
                var vm = new ShoppingCartViewModel();
                foreach (var item in shoppingCartItems)
                {
                    var shoppingCartItem = new ShoppingCartItem();
                    shoppingCartItem.ShoppingCartItemId = item.ShoppingCartItemId;
                    shoppingCartItem.Product = await GetCatalogItemById(item.CatalogItemId);
                    shoppingCartItem.Amount = item.Amount;
                    shoppingCartItem.ShoppingCartId = item.ShoppingCartId;
                };

                return View(vm);
            }

            return NotFound("Shoppingcart not found");
        }

        public async Task<CatalogItemDto> GetCatalogItemById(Guid catalogItemId)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:51044/catalogservice/catalogitems/{catalogItemId}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "AvcPgm.UI");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var catalogItem = await JsonSerializer.DeserializeAsync<CatalogItemDto>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return catalogItem;
            }

            return new CatalogItemDto();
        }

        [Authorize]
        public async Task<IActionResult> AddToShoppingCart(Guid catalogItemId)
        {
            var user = await _userManager.GetUserAsync(User);

            var item = new ShoppingCartItemDto
            {
                CatalogItemId = catalogItemId,
                ShoppingCartId = user.ShoppingCartId
            };

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:51045/shoppingcartservice/shoppingcarts");

            var itemJson = JsonSerializer.Serialize(item);
            request.Content = new StringContent(itemJson, Encoding.UTF8, "application/json");
            request.Headers.Add("User-Agent", "AvcPgm.UI");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                TempData["PostError"] = "Something went wrong, try again or contact support!";
            }

            return RedirectToAction("Index");
        }

        //public RedirectToActionResult RemoveFromShoppingCart(int productId)
        //{
        //    var selectedProduct = _productRepository.AllProducts.FirstOrDefault(p => p.Id == productId);

        //    if (selectedProduct != null)
        //        _shoppingCart.RemoveFromCart(selectedProduct);

        //    return RedirectToAction("Index");
        //}
    }
}