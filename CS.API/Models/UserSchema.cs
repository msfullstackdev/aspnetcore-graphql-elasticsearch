using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.API.Models
{
    public class UserSchema : Schema
    {
        public UserSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<UserQuery>();
        }
    }
}
