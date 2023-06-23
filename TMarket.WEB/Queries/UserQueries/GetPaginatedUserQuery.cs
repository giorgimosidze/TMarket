using System.Collections.Generic;
using MediatR;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Queries.UserQueries
{
    public class GetPaginatedUserQuery : IRequest<IEnumerable<UserRespond>>
    {
        public int CurrentPage { get; } 
        public int PageSize { get; } 
        public string SortBy { get; }
        public bool IsAsc { get; } 

        public GetPaginatedUserQuery(int? currentPage, int? pageSize, string sortBy, bool? isAsc)
        {
            CurrentPage = currentPage ?? 1;
            PageSize = pageSize ?? 5;
            SortBy = string.IsNullOrWhiteSpace(sortBy) ? "Id" : sortBy;
            IsAsc = isAsc ?? true;
        }
    }
}