using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using R2.Routing;

namespace SubscriptionManager.Services.MessageHandlers
{
    public class CommandDelegatingHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var allowedHttpMethods = new[] {HttpMethod.Post, HttpMethod.Put, HttpMethod.Delete};
            var requestMethodIsInvalid = !allowedHttpMethods.Contains(request.Method);

            if (requestMethodIsInvalid)
            {
                throw new NotSupportedException($"{request.Method} request is not supported.");
            }

            var commandName = request.GetRouteData().Values["command"].ToString();
            var commandObjectString = await request.Content.ReadAsStringAsync();

            using (var scope = Container.Instance.BeginLifetimeScope())
            {
                var routeProcessor = scope.Resolve<IRouteProcessor>();

                await routeProcessor.ProcessCommandAsync(commandName, commandObjectString);

                return request.CreateResponse();
            }
        }
    }
}