using MediatR;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Commands.UserCommands
{
    public class UserDeleteCommand : IRequest<UserRespond>
    {
        public int Id { get; }

        public UserDeleteCommand(int id)
        {
            Id = id;
        }
    }
}