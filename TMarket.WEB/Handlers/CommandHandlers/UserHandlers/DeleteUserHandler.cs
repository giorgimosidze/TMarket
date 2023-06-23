using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TMarket.Application.Services.Abstract;
using TMarket.Persistence.DbModels;
using TMarket.WEB.Commands.UserCommands;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Handlers.CommandHandlers.UserHandlers
{
    public class DeleteUserHandler : IRequestHandler<UserDeleteCommand, UserRespond>
    {
        private readonly IBaseService<UserDTO> _userService;
        private readonly IMapper _mapper;

        public DeleteUserHandler(IBaseService<UserDTO> userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserRespond> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.Id);
            if (user == null)
            {
                return null;
            }

            await _userService.DeleteAsync(request.Id);
            return _mapper.Map<UserRespond>(user);
        }
    }
}