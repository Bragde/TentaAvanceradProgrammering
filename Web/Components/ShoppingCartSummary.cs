using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels;

namespace Web.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string _shoppingCartServiceRoot;

        public ShoppingCartSummary(
            IHttpClientFactory clientFactory,
            IConfiguration config,
            UserManager<ApplicationUser> userManager)
        {
            _clientFactory = clientFactory;
            _config = config;
            _userManager = userManager;
            _shoppingCartServiceRoot = _config.GetValue(typeof(string), "ShoppingCartServiceRoot").ToString();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetShoppingCartItems();
            var amount = CalculateShoppingCartItemAmount(items);

            var vm = new ShoppingCartSummaryViewModel
            {
                ItemAmount = amount
            };

            return View(vm);
        }

        private async Task<IEnumerable<ShoppingCartItemDto>> GetShoppingCartItems()
        {
            var user = _userManager.GetUserId(Request.HttpContext.User);

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_shoppingCartServiceRoot}GetShoppingCartItemsByUserId/{user}");
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

        private int CalculateShoppingCartItemAmount(IEnumerable<ShoppingCartItemDto> shoppingCartItems)
        {
            var amount = shoppingCartItems.Select(x => x.Amount).Sum();
            return amount;
        }
    }
}
