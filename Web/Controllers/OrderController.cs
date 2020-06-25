using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly string _shoppingCartServiceRoot;
        private readonly string _orderServiceRoot;
        private readonly string _catalogServiceRoot;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(
            IConfiguration config,
            IHttpClientFactory clientFactory,
            UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _clientFactory = clientFactory;
            _userManager = userManager;
            _shoppingCartServiceRoot = _config.GetValue(typeof(string), "ShoppingCartServiceRoot").ToString();
            _orderServiceRoot = _config.GetValue(typeof(string), "OrderServiceRoot").ToString();
            _catalogServiceRoot = _config.GetValue(typeof(string), "CatalogServiceRoot").ToString();
        }

        public async Task<IActionResult> Purchase()
        {
            // Display view to confirm shipping details
            var user = await _userManager.GetUserAsync(User);

            var vm = new OrderViewModel
            {
                User = user
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                // Get user shoppingcart items and create an order
                var shoppingCartItems = await GetShoppingCartItems(user);

                var orderedProducts = await GetProductInformation(shoppingCartItems);

                var order = CreateOrder(orderedProducts, user);

                // Save order to database
                var client = _clientFactory.CreateClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_orderServiceRoot}CreateOrder");

                var orderJson = JsonSerializer.Serialize(order);
                request.Content = new StringContent(orderJson, Encoding.UTF8, "application/json");
                request.Headers.Add("User-Agent", "AvcPgm.UI");
                var apiKey = _config.GetValue<string>("ApiKeys:OrderApiKey");
                request.Headers.Add("ApiKey", apiKey);
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["PostError"] = "Something went wrong when creating order, try again or contact support!";
                }

                // Remove users shoppingcart
                await DeleteShoppingCart();

                // Get id of object created in database
                var location = response.Headers.Location.ToString();
                var startIdx = location.LastIndexOf('/') + 1;
                var endIdx = location.Length - startIdx;
                var orderId = location.Substring(startIdx, endIdx);
                order.OrderId = Guid.Parse(orderId);

                // Display order confirmation view
                var vm = new OrderViewModel
                {
                     Order = order,
                     User = await _userManager.GetUserAsync(User)
                };

                return View("PurchaseComplete", vm);
            }
            return View(new OrderViewModel());
        }

        private async Task<IEnumerable<ShoppingCartItemDto>> GetShoppingCartItems(ApplicationUser user)
        {
            // Get shoppingcart items by user id
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_shoppingCartServiceRoot}GetShoppingCartItemsByUserId/{user.Id}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "AvcPgm.UI");
            var apiKey = _config.GetValue<string>("ApiKeys:ShoppingCartApiKey");
            request.Headers.Add("ApiKey", apiKey);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var shoppingCartItemsDto = await JsonSerializer.DeserializeAsync<IEnumerable<ShoppingCartItemDto>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return shoppingCartItemsDto;
            }

            return new List<ShoppingCartItemDto>();
        }

        private async Task<IEnumerable<OrderProductDto>> GetProductInformation(IEnumerable<ShoppingCartItemDto> shoppingCartItems)
        {
            // Get catalog information for shoppingcart items
            var orderedProducts = new List<OrderProductDto>();
            foreach (var item in shoppingCartItems)
            {
                var orderedProduct = new OrderProductDto();
                orderedProduct.Product = await GetCatalogItemById(item.CatalogItemId);
                orderedProduct.Amount = item.Amount;

                orderedProducts.Add(orderedProduct);
            }

            return orderedProducts;
        }

        private async Task<CatalogItemDto> GetCatalogItemById(Guid catalogItemId)
        {



            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_catalogServiceRoot}GetById/{catalogItemId}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "AvcPgm.UI");
            var apiKey = _config.GetValue<string>("ApiKeys:CatalogApiKey");
            request.Headers.Add("ApiKey", apiKey);

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

        private OrderDto CreateOrder(IEnumerable<OrderProductDto> orderedProducts, ApplicationUser user)
        {
            // Create new order
            var order = new OrderDto
            {
                UserId = user.Id,
                OrderPlaced = DateTime.Now,
                OrderedProducts = orderedProducts,
                OrderTotal = orderedProducts.Select(x => x.Product.Price * x.Amount).Sum()
            };

            return order;
        }

        private async Task<IActionResult>DeleteShoppingCart()
        {
            var user = await _userManager.GetUserAsync(User);

            // Delete user shoppingcart items
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_shoppingCartServiceRoot}DeleteShoppingCart/{user.Id}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "Gamenet.UI");
            var apiKey = _config.GetValue<string>("ApiKeys:ShoppingCartApiKey");
            request.Headers.Add("ApiKey", apiKey);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Delete shoppingcart items failed.");

            return Ok();
        }
    }
}