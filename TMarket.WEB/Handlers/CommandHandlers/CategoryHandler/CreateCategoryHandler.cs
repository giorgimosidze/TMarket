using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TMarket.Application.Services.Abstract;
using TMarket.Persistence.DbModels;
using TMarket.WEB.Commands.CategoryCommands;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Handlers.CommandHandlers.CategoryHandler
{
    public class CreateCategoryHandler : IRequestHandler<CategoryRequestCommand, CategoryRespond>
    {
        private readonly IMapper _mapper;
        private readonly IBaseService<CategoryDTO> _categoryService;

        public CreateCategoryHandler(IMapper mapper, IBaseService<CategoryDTO> categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public async Task<CategoryRespond> Handle(CategoryRequestCommand request, CancellationToken cancellationToken)
        {
            var user = await _categoryService.InsertAsync(_mapper.Map<CategoryDTO>(request));

            return _mapper.Map<CategoryRespond>(user);
        }
    }
}
