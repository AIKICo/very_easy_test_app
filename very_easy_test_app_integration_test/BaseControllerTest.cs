using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace very_easy_test_app_integration_test
{
    public class BaseControllerTest
    {
        protected readonly HttpClient Client;
        protected string Token;

        public TestContext TestContext { get; set; }

        protected BaseControllerTest()
        {
            Client = TestsHttpClient.Instane;
        }
    }
}