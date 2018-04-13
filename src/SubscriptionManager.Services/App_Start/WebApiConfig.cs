using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Autofac;
using Newtonsoft.Json.Serialization;
using R2;
using R2.Routing;
using SolidServices.Controllerless.WebApi.Description;
using SubscriptionManager.Services.MessageHandlers;

namespace SubscriptionManager.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, ILifetimeScope lifetimeScope)
        {
            UseCamelCaseJsonSerialization(config);

            UseIndentJsonSerialization(config);

            MapRoutes(config);

            UseControllerlessApiDocumentation(config, lifetimeScope);
        }

        private static void UseCamelCaseJsonSerialization(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }

        [Conditional("DEBUG")]
        private static void UseIndentJsonSerialization(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.Indent = true;
        }

        private static void MapRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "CommandApi",
                routeTemplate: "commands/{*command}",
                defaults: null,
                constraints: null,
                handler: new CommandDelegatingHandler()
            );

            config.Routes.MapHttpRoute(
                name: "QueryApi",
                routeTemplate: "queries/{*query}",
                defaults: null,
                constraints: null,
                handler: new QueryDelegatingHandler()
            );

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
        }

        private static void UseControllerlessApiDocumentation(HttpConfiguration config, ILifetimeScope lifetimeScope)
        {
            using (var scope = lifetimeScope.BeginLifetimeScope())
            {
                var routeHandler = scope.Resolve<IRouteHandler>();

                var commandApiExplorer =
                    new ControllerlessApiExplorer(
                        messageTypes: GetKnownCommandTypes(lifetimeScope),
                        responseTypeSelector: type => typeof(void))
                    {
                        ApiPrefix = string.Empty,
                        ControllerName = "commands",
                        ParameterName = "command",
                        ActionNameSelector = commandType => routeHandler.Handle(commandType).First(),
                    };

                var queryApiExplorer = new ControllerlessApiExplorer(
                    messageTypes: GetKnownQueryTypes(lifetimeScope),
                    responseTypeSelector: DetermineResultTypes)
                {
                    ApiPrefix = string.Empty,
                    ControllerName = "queries",
                    ParameterSourceSelector = type => ApiParameterSource.FromUri,
                    HttpMethodSelector = type => HttpMethod.Get,
                    ActionNameSelector = queryType => routeHandler.Handle(queryType).First(),
                };

                config.Services.Replace(
                    typeof(IApiExplorer),
                    new CompositeApiExplorer(
                        config.Services.GetApiExplorer(),
                        commandApiExplorer,
                        queryApiExplorer
                    )
                );
            }
        }

        private static IEnumerable<Type> GetKnownCommandTypes(ILifetimeScope lifetimeScope)
        {
            var commands = lifetimeScope.Resolve<IEnumerable<ICommand>>();

            return
                from command in commands
                select command.GetType();
        }

        private static IEnumerable<Type> GetKnownQueryTypes(ILifetimeScope lifetimeScope)
        {
            var queries = lifetimeScope.Resolve<IEnumerable<IQuery>>();

            return
                from query in queries
                select query.GetType();
        }

        private static Type DetermineResultTypes(Type queryType)
        {
            var resultTypes =
                from interfaceType in queryType.GetInterfaces()
                where interfaceType.IsGenericType
                where interfaceType.GetGenericTypeDefinition() == typeof(IQuery<>)
                select interfaceType.GetGenericArguments()[0];

            return resultTypes.Single();
        }
    }
}