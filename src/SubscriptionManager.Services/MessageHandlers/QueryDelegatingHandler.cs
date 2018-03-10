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
        private static readonly ConcurrentDictionary<string, QueryInfo> QueryInfoMap = GetQueryMap();

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

        private static ConcurrentDictionary<string, QueryInfo> GetQueryMap()
        {
            using (var scope = Container.Instance.BeginLifetimeScope())
            {
                var queryHandlerComponents = scope.Resolve<IEnumerable<IQueryHandler>>();

                var queryHandlerServices =
                    queryHandlerComponents
                        .Select(x => x.GetType())
                        .Select(
                            x => x.GetInterfaces().Single(
                                i => i.IsGenericType &&
                                    i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)));

                var result =
                    queryHandlerServices
                        .Select(
                            service => new QueryInfo
                            {
                                QueryType = service.GenericTypeArguments[0],
                                ResultType = service.GenericTypeArguments[1]
                            })
                        .ToDictionary(
                            queryInfo => queryInfo.QueryType.Name.Replace("Query", string.Empty),
                            queryInfo => queryInfo
                        );

                return new ConcurrentDictionary<string, QueryInfo>(result);
            }
        }

        private class QueryInfo
        {
            private Type _handlerType;

            public Type QueryType { get; set; }

            public Type ResultType { get; set; }

            public Type HandlerType
                => _handlerType ?? (_handlerType = typeof(IQueryHandler<,>).MakeGenericType(QueryType, ResultType));
        }
    }
}