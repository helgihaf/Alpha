using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing.Tests
{
    class PersonResolver : IEvaluationResolver
    {
        private Person person;

        public PersonResolver(Person person)
        {
            this.person = person;
        }

        public object ResolveIdentifier(string identifier)
        {
            object result = null;
            if (identifier == "Person")
            {
                result = person;
            }

            return result;
        }


        public object ResolveArrayMember(string identifier, List<object> indexers)
        {
            throw new NotImplementedException();
        }

        public object ResolveFunctionCall(string functionName, List<object> parameters)
        {
            throw new NotImplementedException();
        }

        public object ResolveMemberInvoke(object obj, MemberInfo memberInfo)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (memberInfo == null)
                throw new ArgumentNullException("memberInfo");

            if (memberInfo.MemberType == MemberType.Function)
            {
                var methodInfo = obj.GetType().GetMethod(memberInfo.Name);
                if (methodInfo == null)
                {
                    throw new MissingMemberException(obj.GetType().FullName, memberInfo.Name);
                }

                return methodInfo.Invoke(obj, memberInfo.Arguments.ToArray());
            }
            
            if (memberInfo.MemberType == MemberType.Property || memberInfo.MemberType == MemberType.Array)
            {
                var propertyInfo = obj.GetType().GetProperty(memberInfo.Name);
                if (propertyInfo == null)
                {
                    throw new MissingMemberException(obj.GetType().FullName, memberInfo.Name);
                }
                object[] index = null;
                if (memberInfo.MemberType == MemberType.Array)
                {
                    index = memberInfo.Arguments.ToArray();
                }
                return propertyInfo.GetValue(obj, index);
            }

            throw new NotImplementedException();
        }
    }
}
