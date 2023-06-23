using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TMarket.Application.Services.Abstract;
using TMarket.Persistence.DbModels;
using TMarket.WEB.Queries.CategoryQueries;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Handlers.QueryHandlers.CategoryHandler
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryRespond>
    {
        private readonly IBaseService<CategoryDTO> _categoryService;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IBaseService<CategoryDTO> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<CategoryRespond> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var user = await _categoryService.GetByIdAsync(request.CategoryId);
            return user != null
                ? _mapper.Map<CategoryRespond>(user)
                : null;
        }
    }
}
