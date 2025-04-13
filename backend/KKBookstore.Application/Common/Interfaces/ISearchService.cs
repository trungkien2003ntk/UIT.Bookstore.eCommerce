using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Common.Interfaces;

public interface ISearchService
{
    Task<SearchResult> SearchAsync(SearchRequest searchRequest);
}
