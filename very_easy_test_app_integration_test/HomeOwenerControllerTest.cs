using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace very_easy_test_app_integration_test
{
    [TestClass]
    public class HomeOwenerControllerTest:BaseControllerTest
    {
        [TestMethod]
        public async Task Get()
        {
            var response = await Client.GetAsync("/DTOHomeOwner");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

    }
}