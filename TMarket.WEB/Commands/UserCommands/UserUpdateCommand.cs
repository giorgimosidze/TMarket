using MediatR;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Commands.UserCommands
{
    public class UserUpdateCommand : IRequest<UserRespond>
    {
        public UserRequestCommand Command { get; }
        public int Id { get; }

        public UserUpdateCommand(UserRequestCommand command, int id)
        {
            Command = command;
            Id = id;
        }
    }
}