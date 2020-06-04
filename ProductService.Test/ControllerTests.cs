using CatalogService.Controllers;
using CatalogService.Data.Context;
using CatalogService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CatalogService.Test
{
    public class ControllerTests
    {
        [Fact]
        public async Task GetAllCatalogItems_ReturnsOk()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("CatalogService/CatalogItem/GetAll");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact] async Task GetCatalogItemById_ReturnsNotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("CatalogService/CatalogItem/GetById/" + Guid.Empty);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
