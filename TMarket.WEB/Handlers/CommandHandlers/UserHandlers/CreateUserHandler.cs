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
    public class CreateUserHandler : IRequestHandler<UserRequestCommand, UserRespond>
    {
        private readonly IMapper _mapper;
        private readonly IBaseService<UserDTO> _userService;

        public CreateUserHandler(IMapper mapper, IBaseService<UserDTO> userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<UserRespond> Handle(UserRequestCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.InsertAsync(_mapper.Map<UserDTO>(request));

            return _mapper.Map<UserRespond>(user);
        }
    }
}