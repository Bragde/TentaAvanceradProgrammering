using CatalogService.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CatalogService.Test
{
    class TestClientProvider : IDisposable
    {
        public TestServer Server { get; private set; }
        public HttpClient Client { get; private set; }

        public TestClientProvider()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            WebHostBuilder webHostBuilder = new WebHostBuilder();
            webHostBuilder.ConfigureServices(s => s.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))));

            webHostBuilder.UseStartup<Startup>();

            Server = new TestServer(webHostBuilder);

            Client = Server.CreateClient();
        }
        public void Dispose()
        {
            Server?.Dispose();
            Client?.Dispose();
        }
    }
}
