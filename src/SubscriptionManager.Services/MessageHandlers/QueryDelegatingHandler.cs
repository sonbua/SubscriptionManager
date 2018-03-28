using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using R2.Routing;

namespace SubscriptionManager.Services.MessageHandlers
{
    public class QueryDelegatingHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var allowedHttpMethods = new[] {HttpMethod.Get, HttpMethod.Head, HttpMethod.Options};
            var requestMethodIsInvalid = !allowedHttpMethods.Contains(request.Method);

            if (requestMethodIsInvalid)
            {
                throw new NotSupportedException($"{request.Method} request is not supported.");
            }

            var queryName = request.GetRouteData().Values["query"].ToString();
            var queryObjectString = SerializationHelpers.ConvertQueryStringToJson(request.RequestUri.Query);

            using (var scope = Container.Instance.BeginLifetimeScope())
            {
                var routeProcessor = scope.Resolve<IRouteProcessor>();

                var result = await routeProcessor.ProcessQueryAsync(queryName, queryObjectString);

                return request.CreateResponse(HttpStatusCode.OK, result, new JsonMediaTypeFormatter());
            }
        }
    }
}