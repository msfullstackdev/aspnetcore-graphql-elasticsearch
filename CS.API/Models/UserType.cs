using CS.DomainEntity.Entity;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.API.Models
{
    public class UserType: ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id);
            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Email);
        }
    }
}
