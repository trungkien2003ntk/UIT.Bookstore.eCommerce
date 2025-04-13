using KKBookstore.Application.Common.Models.RequestDtos;
using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Common.Interfaces;

public interface ISearchService
{
    Task<SearchResult> SearchAsync(SearchRequest searchRequest);
}
