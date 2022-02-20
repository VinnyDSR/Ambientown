using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;

namespace Tests.Fixtures
{
    public class IntegrationFixture<TStartup> : IDisposable where TStartup : class
    {
        private readonly ApiAppFactory<TStartup> Factory;

        public HttpClient Client { get; }

        public IntegrationFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost:44310"),
            };

            Factory = new ApiAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}