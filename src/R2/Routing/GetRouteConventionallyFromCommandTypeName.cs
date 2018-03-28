using System;
using System.Collections.Generic;

namespace R2.Routing
{
    public class GetRouteConventionallyFromCommandTypeName : ResponsibilityChain<Type, IEnumerable<string>>
    {
        public override bool CanHandle(Type commandType) => commandType.Name.EndsWith("Command");

        public override IEnumerable<string> Handle(Type commandType)
        {
            yield return commandType.Name.Substring(0, commandType.Name.Length - 7);
        }
    }
}