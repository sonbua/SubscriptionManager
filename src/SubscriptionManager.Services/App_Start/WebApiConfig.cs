using System.Web.Http;
using SubscriptionManager.Services.MessageHandlers;

namespace SubscriptionManager.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Command",
                routeTemplate: "commands/{command}",
                defaults: null,
                constraints: null,
                handler: new CommandDelegatingHandler()
            );

            config.Routes.MapHttpRoute(
                name: "Query",
                routeTemplate: "queries/{query}",
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
    }
}