using MediatR;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Commands.CategoryCommands
{
    public class CategoryUpdateCommand : IRequest<CategoryRespond>
    {
        public CategoryRequestCommand Command { get; }
        public int Id { get; }

        public CategoryUpdateCommand(CategoryRequestCommand command, int id)
        {
            Command = command;
            Id = id;
        }
    }
}
