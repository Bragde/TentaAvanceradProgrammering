using ShoppingCartService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingCartService.Test
{
    public class ControllerTests
    {
        [Fact]
        public async Task GetShoppingCartItemsByUserId_ReturnsNotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("ApiKey", "MySecretShoppingCartApiKey");

                // Provide userid for user that has no shoppingcart (user does not exist)
                var response = await client.GetAsync("ShoppingCartService/ShoppingCart/GetShoppingCartItemsByUserId/0000-0000-0000-0000-0000");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetShoppingCartItemsByUserId_ReturnsOK()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("ApiKey", "MySecretShoppingCartApiKey");

                /* The used userid should be created in Web/Data/ApplicationDbInitializer and a shopping cart 
                   for the user should be created in ShoppingCartService/Data/Context/ShoppingCartDbContext */
                var response = await client.GetAsync("ShoppingCartService/ShoppingCart/GetShoppingCartItemsByUserId/d28888e9-2ba9-473a-a40f-e38cb54f9b35");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task AddItemToShoppingCart_ReturnsOK()
        {
            var item = new ShoppingCartItemDto
            {
                UserId = "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                CatalogItemId = Guid.Parse("90d6da79-e0e2-4ba8-bf61-2d94d90df801"),
                Amount = 1
            };

            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("ApiKey", "MySecretShoppingCartApiKey");
                var request = new HttpRequestMessage(HttpMethod.Post, $"ShoppingCartService/ShoppingCart/AddItemToShoppingCart");
                var orderJson = JsonSerializer.Serialize(item);
                request.Content = new StringContent(orderJson, Encoding.UTF8, "application/json");
                request.Headers.Add("User-Agent", "AvcPgm.ShoppingCartService.Test");
                var response = await client.SendAsync(request);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task AddItemToShoppingCart_ReturnsBadRequest()
        {
            var item = new ShoppingCartItemDto{};

            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("ApiKey", "MySecretShoppingCartApiKey");
                var request = new HttpRequestMessage(HttpMethod.Post, $"ShoppingCartService/ShoppingCart/AddItemToShoppingCart");
                var orderJson = JsonSerializer.Serialize(item);
                request.Content = new StringContent(orderJson, Encoding.UTF8, "application/json");
                request.Headers.Add("User-Agent", "AvcPgm.ShoppingCartService.Test");
                var response = await client.SendAsync(request);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task DeleteShoppingCart_ReturnsOK()
        {
            using (var client = new TestClientProvider().Client)
            {
                // Add a test item to the shoppingcart database
                var item = new ShoppingCartItemDto
                {
                    UserId = "d28888e9-2ba9-473a-a40f-e38cb54f9b99",
                    CatalogItemId = Guid.Parse("90d6da79-e0e2-4ba8-bf61-2d94d90df899"),
                    Amount = 1
                };

                client.DefaultRequestHeaders.Add("ApiKey", "MySecretShoppingCartApiKey");
                var request = new HttpRequestMessage(HttpMethod.Post, $"ShoppingCartService/ShoppingCart/AddItemToShoppingCart");
                var orderJson = JsonSerializer.Serialize(item);
                request.Content = new StringContent(orderJson, Encoding.UTF8, "application/json");
                request.Headers.Add("User-Agent", "AvcPgm.ShoppingCartService.Test");
                var responseAdd = await client.SendAsync(request);
                responseAdd.EnsureSuccessStatusCode();
               
                // Delete item from database
                var responseDel = await client.DeleteAsync("ShoppingCartService/ShoppingCart/DeleteShoppingCart/d28888e9-2ba9-473a-a40f-e38cb54f9b99");
                responseDel.EnsureSuccessStatusCode();

                // Assert result of delete
                Assert.Equal(HttpStatusCode.OK, responseDel.StatusCode);
            }
        }

        [Fact]
        // Test that an http request to the controller without correct apikey will not be allowed access.
        public async Task HttpRequest_ReturnsUnauthorized()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("ShoppingCartService/ShoppingCart/GetShoppingCartItemsByUserId/0000-0000-0000-0000-0000");

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }
    }
}
