using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SilverHorseTech.WebApi.SelfHost
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
            bool isValid =
                message.Headers.Authorization != null &&
                string.Equals("Bearer af24353tdsfw", message.Headers.Authorization.ToString(), StringComparison.InvariantCulture);
            return isValid;
        }
    }
}
