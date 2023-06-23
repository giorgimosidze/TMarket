using System.Collections.Generic;
using MediatR;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Queries.UserQueries
{
    public class GetAllUserQuery : IRequest<IEnumerable<UserRespond>>
    {
        
    }
}