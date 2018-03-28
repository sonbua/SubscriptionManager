using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace R2.Routing
{
    public class CommandRouteTable : IRouteTable
    {
        private static ConcurrentDictionary<string, RouteEntry> _map;

        private readonly IEnumerable<ICommand> _commandComponents;
        private readonly ResponsibilityChain<Type, IEnumerable<string>> _routeHandler;

        public CommandRouteTable(
            IEnumerable<ICommand> commandComponents,
            ResponsibilityChain<Type, IEnumerable<string>> routeHandler)
        {
            _commandComponents = commandComponents;
            _routeHandler = routeHandler;
        }

        public ConcurrentDictionary<string, RouteEntry> Table => _map ?? (_map = InitializeTable());

        private ConcurrentDictionary<string, RouteEntry> InitializeTable()
        {
            var routeEntries =
                from component in _commandComponents
                let componentType = component.GetType()
                where componentType.Name.EndsWith("Command")
                from routePath in _routeHandler.Handle(componentType)
                select new RouteEntry
                {
                    RoutePath = routePath,
                    RequestType = componentType,
                    HandlerType = typeof(ICommandHandler<>).MakeGenericType(componentType)
                };

            return new ConcurrentDictionary<string, RouteEntry>(
                routeEntries.ToDictionary(routeEntry => routeEntry.RoutePath, routeEntry => routeEntry)
            );
        }
    }
}