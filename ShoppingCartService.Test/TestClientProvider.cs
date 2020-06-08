using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCartService;
using ShoppingCartService.Data.Context;
using System;
using System.Net.Http;

namespace ShoppingCartService.Test
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
            webHostBuilder.ConfigureServices(s => s.AddDbContext<ShoppingCartDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))));

            webHostBuilder.UseStartup<Startup>();
            webHostBuilder.UseConfiguration(configuration);

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
