using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SilverHorseTech.WebApi.Handlers;

namespace SilverHorseTech.WebApi.Tests.Handlers
{
    [TestClass]
    public class AuthorizationHeaderTest
    {
        [TestMethod]
        public async Task NoAuthorizationHeaderShouldReturn501()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, " https://jsonplaceholder.typicode.com");
            var invoker = new HttpMessageInvoker(new AuthorizationHeaderHandler());
            HttpResponseMessage responseMessage = await invoker.SendAsync(httpRequestMessage, new CancellationToken());

            Assert.IsNotNull(responseMessage);
            Assert.AreEqual(HttpStatusCode.NotImplemented, responseMessage.StatusCode);
        }

        [TestMethod]
        public async Task InvalidAuthorizationHeaderValueShouldReturn501()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, " https://jsonplaceholder.typicode.com");

            // use the actual expected value but converted to uppercase
            // this ensure case sensitive string comparison is being applied
            string headerValue = ConfigurationManager.AppSettings["authorizationvalue"];
            httpRequestMessage.Headers.Authorization = AuthenticationHeaderValue.Parse(headerValue.ToUpper());
            var invoker = new HttpMessageInvoker(new AuthorizationHeaderHandler());
            HttpResponseMessage responseMessage = await invoker.SendAsync(httpRequestMessage, new CancellationToken());

            Assert.IsNotNull(responseMessage);
            Assert.AreEqual(HttpStatusCode.NotImplemented, responseMessage.StatusCode);
        }

        [TestMethod]
        public async Task ValidAuthorizationHeaderValueShouldReturn200()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, " https://jsonplaceholder.typicode.com");
            string headerValue = ConfigurationManager.AppSettings["authorizationvalue"];
            httpRequestMessage.Headers.Authorization = AuthenticationHeaderValue.Parse(headerValue);
            var handler = new AuthorizationHeaderHandler()
            {
                InnerHandler = new Mock<HttpMessageHandler>(MockBehavior.Loose).Object
            };

            var invoker = new HttpMessageInvoker(handler);
            // on success we will get a null object back (due to using MockBehavior.Loose)
            Assert.IsNull(await invoker.SendAsync(httpRequestMessage, new CancellationToken()));
        }

    }
}
