using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TMarket.Application.Services.Abstract;
using TMarket.Persistence.DbModels;
using TMarket.WEB.Commands.UserCommands;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Handlers.CommandHandlers.UserHandlers
{
    public class UpdateUserHandler : IRequestHandler<UserUpdateCommand, UserRespond>
    {
        private readonly IBaseService<UserDTO> _userService;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IBaseService<UserDTO> userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserRespond> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsyncWithNoTracking();
            if (!users.Any(x => x.Id == request.Id))
            {
                return null;
            }

            var user = _mapper.Map<UserDTO>(request);
            var updatedUser =  await _userService.UpdateAsync(user);
            return _mapper.Map<UserRespond>(updatedUser);
        }
    }
}