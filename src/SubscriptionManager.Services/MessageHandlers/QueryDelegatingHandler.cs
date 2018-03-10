using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using R2;

namespace SubscriptionManager.Services.MessageHandlers
{
    public class QueryDelegatingHandler : HttpMessageHandler
    {
        private static readonly ConcurrentDictionary<string, QueryInfo> QueryInfoMap = GetQueryInfoMap();

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
            var queryInfo = QueryInfoMap[queryName];

            var queryObjectString = SerializationHelpers.ConvertQueryStringToJson(request.RequestUri.Query);
            var queryObject = JsonConvert.DeserializeObject(queryObjectString, queryInfo.QueryType);

            using (var scope = Container.Instance.BeginLifetimeScope())
            {
                var requestProcessor = scope.Resolve<IRequestProcessor>();

                var result = await requestProcessor.ProcessQueryAsync(queryObject, queryInfo.HandlerType);

                return request.CreateResponse(HttpStatusCode.OK, result, new JsonMediaTypeFormatter());
            }
        }

        private static ConcurrentDictionary<string, QueryInfo> GetQueryInfoMap()
        {
            using (var scope = Container.Instance.BeginLifetimeScope())
            {
                var queryComponents = scope.Resolve<IEnumerable<IQuery>>();

                var queryInfos =
                    from component in queryComponents
                    let componentType = component.GetType()
                    let serviceType = componentType.GetInterfaces().Single(
                        i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuery<>)
                    )
                    let resultType = serviceType.GenericTypeArguments[0]
                    let handlerType = typeof(IQueryHandler<,>).MakeGenericType(componentType, resultType)
                    select new QueryInfo
                    {
                        QueryType = componentType,
                        HandlerType = handlerType
                    };

                var result =
                    queryInfos
                        .ToDictionary(
                            queryInfo => queryInfo.QueryType.Name.Replace("Query", string.Empty),
                            queryInfo => queryInfo
                        );

                return new ConcurrentDictionary<string, QueryInfo>(result);
            }
        }

        private class QueryInfo
        {
            public Type QueryType { get; set; }

            public Type HandlerType { get; set; }
        }
    }
}