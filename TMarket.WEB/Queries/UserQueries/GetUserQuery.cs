using MediatR;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Queries.UserQueries
{
    public class GetUserQuery : IRequest<UserRespond>
    {
        public int UserId { get; }

        public GetUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}