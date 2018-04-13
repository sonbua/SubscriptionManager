using System;
using System.Collections.Generic;
using R2.Routing;
using R2.Routing.AspNetCore;
using ResponsibilityChain;

namespace SubscriptionManager.Services.DependencyRegistration.Autofac
{
    public class RouteHandler : CompositeChainHandler<Type, IEnumerable<string>>, IRouteHandler
    {
        public RouteHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            AddHandler<GetRouteFromRouteAttribute>();
            AddHandler<GetRouteConventionallyFromCommandTypeNameWithoutSuffix>();
            AddHandler<GetRouteConventionallyFromQueryTypeNameWithoutSuffix>();
            AddHandler<GetRouteFromCommandTypeName>();
            AddHandler<GetRouteFromQueryTypeName>();
        }
    }
}