using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SubscriptionManager.Services.MessageHandlers
{
    public class QueryDelegatingHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}