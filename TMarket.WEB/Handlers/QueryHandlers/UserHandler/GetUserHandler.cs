using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TMarket.Application.Services.Abstract;
using TMarket.Persistence.DbModels;
using TMarket.WEB.Queries.UserQueries;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Handlers.QueryHandlers.UserHandler
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserRespond>
    {
        private readonly IBaseService<UserDTO> _userService;
        private readonly IMapper _mapper;

        public GetUserHandler(IBaseService<UserDTO> userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserRespond> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.UserId);
            return user != null 
                ? _mapper.Map<UserRespond>(user) 
                : null;
        }
    }
}