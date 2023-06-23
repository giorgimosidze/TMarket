using MediatR;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Queries.CategoryQueries
{
    public class GetCategoryQuery : IRequest<CategoryRespond>
    {
        public int CategoryId { get; }

        public GetCategoryQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
