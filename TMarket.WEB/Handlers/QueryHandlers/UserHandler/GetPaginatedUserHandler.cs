using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TMarket.Application.Services.Abstract;
using TMarket.Persistence.DbModels;
using TMarket.WEB.Helpers.CustomExceptions;
using TMarket.WEB.Queries.UserQueries;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Handlers.QueryHandlers.UserHandler
{
    public class GetPaginatedUserHandler : IRequestHandler<GetPaginatedUserQuery, IEnumerable<UserRespond>>
    {
        private readonly IBaseService<UserDTO> _userService;
        private readonly IMapper _mapper;

        public GetPaginatedUserHandler(IBaseService<UserDTO> userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserRespond>> Handle(GetPaginatedUserQuery request, CancellationToken cancellationToken)
        {
            if (request.CurrentPage < 1 || request.PageSize < 1 || typeof(UserRespond).GetProperty(request.SortBy) == null)
            {
                throw new InvalidArgumentException();
            }

            var users = await _userService
                .GetPaginatedResultAsyncAsNoTracking(request.CurrentPage, request.PageSize, request.SortBy, request.IsAsc);
            return _mapper.Map<List<UserRespond>>(users);
        }
    }
}