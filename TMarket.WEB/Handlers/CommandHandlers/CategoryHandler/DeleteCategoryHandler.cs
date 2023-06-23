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
    public class DeleteCategoryHandler : IRequestHandler<CategoryDeleteCommand, CategoryRespond>
    {
        private readonly IBaseService<CategoryDTO> _categoryService;
        private readonly IMapper _mapper;

        public DeleteCategoryHandler(IBaseService<CategoryDTO> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<CategoryRespond> Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetByIdAsync(request.Id);

            if (category == null)
            {
                return null;
            }

            await _categoryService.DeleteAsync(request.Id);
            return _mapper.Map<CategoryRespond>(category);
        }
    }
}
