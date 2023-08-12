using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using BookDataBase;

namespace BookDataBase.Tests
{
    public class IntegrationTestBase : IDisposable
    {
        protected readonly HttpClient TestClient;

        public IntegrationTestBase()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        // Replace or mock services here if needed
                    });
                });

            TestClient = appFactory.CreateClient();
        }

        public void Dispose()
        {
            TestClient.Dispose();
        }
    }
}
