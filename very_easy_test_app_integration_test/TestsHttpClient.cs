using System;
using System.Net.Http;
using System.Threading;

namespace very_easy_test_app_integration_test
{
    public static class TestsHttpClient
    {
        private static readonly Lazy<HttpClient> _serviceProviderBuilder =
            new Lazy<HttpClient>(getHttpClient, LazyThreadSafetyMode.ExecutionAndPublication);

        public static HttpClient Instane => _serviceProviderBuilder.Value;
        private static HttpClient getHttpClient()
        {
            var services = new CustomWebApplicationFactory();
            return services.CreateClient();
        }
    }
}