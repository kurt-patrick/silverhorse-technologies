using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SilverHorseTech.WebApi.Handlers
{
    public class AuthorizationHeaderHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!IsAuthorizationHeaderValid(request))
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }

        private bool IsAuthorizationHeaderValid(HttpRequestMessage message)
        {
            string authorizationValue = ConfigurationManager.AppSettings["authorizationvalue"];
            if (string.IsNullOrWhiteSpace(authorizationValue))
            {
                throw new ArgumentOutOfRangeException("authorizationvalue");
            }

            bool isValid =
                message.Headers.Authorization != null &&
                string.Equals(authorizationValue, message.Headers.Authorization.ToString(), StringComparison.InvariantCulture);
            return isValid;
        }

    }
}