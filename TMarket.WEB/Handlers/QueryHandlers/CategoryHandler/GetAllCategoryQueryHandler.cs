using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMarket.Application.Services.Abstract;
using TMarket.Persistence.DbModels;
using TMarket.WEB.Queries.CategoryQueries;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Handlers.QueryHandlers.CategoryHandler
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<CategoryRespond>>
    {
        private readonly IBaseService<CategoryDTO> _categoryService;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(IBaseService<CategoryDTO> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryRespond>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var items = await _categoryService.GetAllAsyncWithNoTracking();

            return _mapper.Map<List<CategoryRespond>>(items);
        }
    }
}
