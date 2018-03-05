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
        private static readonly ConcurrentDictionary<string, CommandInfo> CommandInfoMap = GetCommandMap();

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var commandName = request.GetRouteData().Values["command"].ToString();
            var requestInfo = CommandInfoMap[commandName];

            var requestBody = await request.Content.ReadAsStringAsync();
            var requestObjectString = "{" + requestBody + "}";
            var requestObject = JsonConvert.DeserializeObject(requestObjectString, requestInfo.CommandType);

            using (var scope = Container.Instance.BeginLifetimeScope())
            {
                var requestProcessor = scope.Resolve<IRequestProcessor>();

                var responseObject = await requestProcessor.ProcessAsync(requestObject, requestInfo.HandlerType);

                return request.CreateResponse(JsonConvert.SerializeObject(responseObject));
            }
        }

        private static ConcurrentDictionary<string, CommandInfo> GetCommandMap()
        {
            using (var scope = Container.Instance.BeginLifetimeScope())
            {
                var requestHandlerComponents = scope.Resolve<IEnumerable<IRequestHandler>>();

                var requestHandlerServices =
                    requestHandlerComponents
                        .Select(x => x.GetType())
                        .Select(
                            x => x.GetInterfaces().First(
                                i => i.IsGenericType &&
                                     i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)));
                var result =
                    requestHandlerServices
                        .Select(service => service.GetGenericArguments()[0])
                        .Select(
                            commandType => new
                            {
                                CommandName = commandType.Name.Replace("Command", string.Empty),
                                CommandInfo = new CommandInfo {CommandType = commandType}
                            }).ToDictionary(x => x.CommandName, x => x.CommandInfo);

                return new ConcurrentDictionary<string, CommandInfo>(result);
            }
        }

        private class CommandInfo
        {
            public Type CommandType { get; set; }

            public Type HandlerType =>
                typeof(IRequestHandler<,>).MakeGenericType(CommandType, typeof(Nothing<>).MakeGenericType(CommandType));
        }
    }
}