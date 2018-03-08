using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using R2;

namespace SubscriptionManager.Services.MessageHandlers
{
    public class CommandDelegatingHandler : HttpMessageHandler
    {
        private static readonly ConcurrentDictionary<string, Type> CommandMap = GetCommandMap();

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
            var commandType = CommandMap[commandName];

            var requestBody = await request.Content.ReadAsStringAsync();
            var commandObjectString = "{" + requestBody + "}";
            var commandObject = JsonConvert.DeserializeObject(commandObjectString, commandType);

            using (var scope = Container.Instance.BeginLifetimeScope())
            {
                var requestProcessor = scope.Resolve<IRequestProcessor>();

                await requestProcessor.ProcessCommandAsync(commandObject);

                return request.CreateResponse();
            }
        }

        private static ConcurrentDictionary<string, Type> GetCommandMap()
        {
            using (var scope = Container.Instance.BeginLifetimeScope())
            {
                var commandHandlerComponents = scope.Resolve<IEnumerable<ICommandHandler>>();

                var commandHandlerServices =
                    commandHandlerComponents
                        .Select(x => x.GetType())
                        .Select(
                            x => x.GetInterfaces().Single(
                                i => i.IsGenericType &&
                                    i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));

                var result =
                    commandHandlerServices
                        .Select(service => service.GetGenericArguments()[0])
                        .ToDictionary(
                            commandType => commandType.Name.Replace("Command", string.Empty),
                            commandType => commandType
                        );

                return new ConcurrentDictionary<string, Type>(result);
            }
        }
    }
}