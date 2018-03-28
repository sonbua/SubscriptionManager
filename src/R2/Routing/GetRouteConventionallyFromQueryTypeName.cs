using System;
using System.Collections.Generic;

namespace R2.Routing
{
    public class GetRouteConventionallyFromQueryTypeName : ResponsibilityChain<Type, IEnumerable<string>>
    {
        public override bool CanHandle(Type queryType) => queryType.Name.EndsWith("Query");

        public override IEnumerable<string> Handle(Type queryType)
        {
            yield return queryType.Name.Substring(0, queryType.Name.Length - 5);
        }
    }
}