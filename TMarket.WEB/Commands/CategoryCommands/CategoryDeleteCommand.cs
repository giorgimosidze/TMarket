using MediatR;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Commands.CategoryCommands
{
    public class CategoryDeleteCommand : IRequest<CategoryRespond>
    {
        public int Id { get; }

        public CategoryDeleteCommand(int id)
        {
            Id = id;
        }
    }
}
