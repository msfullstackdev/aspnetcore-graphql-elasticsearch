using CS.DomainEntity.Contracts.IInfrastructre;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.API.Models
{
    public class UserQuery: ObjectGraphType
    {

        public UserQuery(IUserServices userServices)
        {

            Field<UserType>(
                "player",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => userServices.GetByIdAsync(context.GetArgument<string>("id")));

            Field<ListGraphType<UserType>>(
                "users",
                resolve: context => userServices.GetAllAsync());



        }

    }
}
