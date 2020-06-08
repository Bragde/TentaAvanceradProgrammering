using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace OrderService.Test
{
    public class ControllerTests
    {
        [Fact]
        public async Task CreateOrder_ReturnsCreated()
        {
            using (var client = new TestClientProvider().Client)
            {
                var order = new OrderDto
                {
                    UserId = "d28888e9-2ba9-473a-a40f-e38cb54f0201",
                    OrderPlaced = new DateTime(2020, 06, 03),
                    OrderedProducts = new List<OrderProductDto>{
                        new OrderProductDto {
                        Product = new CatalogItemDto { Id = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"), Title = "Arizona Sunshine", Price = 39.99M },
                        Amount = 1 }},
                     OrderTotal = 39.99M
                };

                client.DefaultRequestHeaders.Add("ApiKey", "MySecretOrderApiKey");
                var request = new HttpRequestMessage(HttpMethod.Post, $"OrderService/Order/CreateOrder");
                var orderJson = JsonSerializer.Serialize(order);
                request.Content = new StringContent(orderJson, Encoding.UTF8, "application/json");
                request.Headers.Add("User-Agent", "AvcPgm.OrderService.Test");
                var response = await client.SendAsync(request);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        [Fact]
        public async Task CreateOrder_ReturnsBadRequest()
        {
            using (var client = new TestClientProvider().Client)
            {
                var order = new OrderDto();

                client.DefaultRequestHeaders.Add("ApiKey", "MySecretOrderApiKey");
                var request = new HttpRequestMessage(HttpMethod.Post, $"OrderService/Order/CreateOrder");
                var orderJson = JsonSerializer.Serialize(order);
                request.Content = new StringContent(orderJson, Encoding.UTF8, "application/json");
                request.Headers.Add("User-Agent", "AvcPgm.OrderService.Test");
                var response = await client.SendAsync(request);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        // Test that an http request to the controller without correct apikey will not be allowed access.
        public async Task HttpRequest_ReturnsUnauthorized()
        {
            using (var client = new TestClientProvider().Client)
            {
                var order = new OrderDto();

                var request = new HttpRequestMessage(HttpMethod.Post, $"OrderService/Order/CreateOrder");
                var orderJson = JsonSerializer.Serialize(order);
                request.Content = new StringContent(orderJson, Encoding.UTF8, "application/json");
                request.Headers.Add("User-Agent", "AvcPgm.OrderService.Test");
                var response = await client.SendAsync(request);

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }
    }
}
