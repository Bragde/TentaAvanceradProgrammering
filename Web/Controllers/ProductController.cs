using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly string _catalogServiceRoot;

        public ProductController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
            _catalogServiceRoot = _config.GetValue<string>("CatalogServiceRoot").ToString();
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_catalogServiceRoot}GetAll");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "AvcPgm.UI");
            var apiKey = _config.GetValue<string>("ApiKeys:CatalogApiKey");
            request.Headers.Add("ApiKey", apiKey);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var catalogItems = await JsonSerializer.DeserializeAsync<IEnumerable<CatalogItemDto>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                
                var vm = new ProductViewModel() { Products = catalogItems };

                return View(vm);
            }

            return View(new ProductViewModel());
        }

        public async Task<IActionResult> GetById(Guid catalogItemId)
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

                return View(catalogItem);
            }

            return RedirectToAction("Index");
        }
    }
}
