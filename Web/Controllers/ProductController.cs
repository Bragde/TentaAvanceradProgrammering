using Microsoft.AspNetCore.Mvc;
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

        public ProductController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:51044/catalogservice/CatalogItem/GetAll");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "AvcPgm.UI");

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
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:51044/catalogservice/CatalogItem/GetById/{catalogItemId}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "AvcPgm.UI");

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
