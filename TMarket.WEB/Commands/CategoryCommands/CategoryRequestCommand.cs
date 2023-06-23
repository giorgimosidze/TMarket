using MediatR;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Commands.CategoryCommands
{
    public class CategoryRequestCommand : IRequest<CategoryRespond>
    {
        public string Name { get; set; }
    }
}
