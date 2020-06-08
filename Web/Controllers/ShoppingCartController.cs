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
    public class ShoppingCartController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly string _shoppingCartServiceRoot;
        private readonly string _catalogServiceRoot;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(
            IHttpClientFactory clientFactory,
            IConfiguration config,
            UserManager<ApplicationUser> userManager)
        {
            _clientFactory = clientFactory;
            _config = config;
            _userManager = userManager;
            _shoppingCartServiceRoot = _config.GetValue(typeof(string), "ShoppingCartServiceRoot").ToString();
            _catalogServiceRoot = _config.GetValue(typeof(string), "CatalogServiveRoot").ToString();                      
        }

        public async Task<CatalogItemDto> GetCatalogItemById(Guid catalogItemId)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_catalogServiceRoot}GetById/{catalogItemId}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "AvcPgm.UI");
            var apiKey = _config.GetValue<string>("ApiKeys:MySecretApiKey");
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

        [Authorize]
        public async Task<IActionResult> AddToShoppingCart(Guid catalogItemId)
        {
            var user = await _userManager.GetUserAsync(User);

            // Create a new shoppingcart item dto
            var item = new ShoppingCartItemDto
            {
                CatalogItemId = catalogItemId,
                UserId = user.Id
            };

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_shoppingCartServiceRoot}AddItemToShoppingCart");

            var itemJson = JsonSerializer.Serialize(item);
            request.Content = new StringContent(itemJson, Encoding.UTF8, "application/json");
            request.Headers.Add("User-Agent", "AvcPgm.UI");
            var apiKey = _config.GetValue<string>("ApiKeys:ShoppingCartApiKey");
            request.Headers.Add("ApiKey", apiKey);
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                TempData["PostError"] = "Something went wrong when adding to shoppingcart, try again or contact support!";
            }

            return RedirectToAction("DisplayShoppingCart");
        }

        public async Task<IActionResult> DisplayShoppingCart()
        {
            var shoppingCartItemsDto = await GetShoppingCartItems();
            if (shoppingCartItemsDto == null)
                return NotFound("Shoppingcart not found");

            // Create shoppingcart viewmodel
            var vm = new ShoppingCartViewModel();
            foreach (var item in shoppingCartItemsDto)
            {
                var shoppingCartItem = new ShoppingCartItem(item);
                shoppingCartItem.Product = await GetCatalogItemById(item.CatalogItemId);
                vm.ShoppingCartItems.Add(shoppingCartItem);
            };
            vm.ShoppingCartTotal = vm.ShoppingCartItems
                .Select(x => x.Product.Price * x.Amount)
                .Sum();

            return View(vm);
        }

        internal async Task<IEnumerable<ShoppingCartItemDto>> GetShoppingCartItems()
        {
            var user = await _userManager.GetUserAsync(User);

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
    }
}