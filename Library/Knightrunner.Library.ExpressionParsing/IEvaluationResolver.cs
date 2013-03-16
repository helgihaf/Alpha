using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing
{
    public interface IEvaluationResolver
    {
        object ResolveIdentifier(string identifier);
        object ResolveArrayMember(string identifier, List<object> indexers);
        object ResolveFunctionCall(string functionName, List<object> parameters);

        object ResolveMemberInvoke(object obj, MemberInfo memberInfo);
    }
}
