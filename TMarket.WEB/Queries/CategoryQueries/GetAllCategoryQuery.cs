using MediatR;
using System.Collections.Generic;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Queries.CategoryQueries
{
    public class GetAllCategoryQuery : IRequest<IEnumerable<CategoryRespond>>
    {
    }
}
