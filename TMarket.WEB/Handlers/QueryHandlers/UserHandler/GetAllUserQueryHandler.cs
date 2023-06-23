using System.Collections.Generic;
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
    public class GetAllQueryHandler 
        : IRequestHandler<GetAllUserQuery, IEnumerable<UserRespond>>
    {
        private readonly IBaseService<UserDTO> _userService;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IBaseService<UserDTO> userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<UserRespond>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var items = await _userService.GetAllAsyncWithNoTracking();

            return _mapper.Map<List<UserRespond>>(items);
        }
    }
}