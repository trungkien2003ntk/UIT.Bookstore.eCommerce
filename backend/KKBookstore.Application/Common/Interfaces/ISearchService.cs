using KKBookstore.Application.Common.Models.RequestDtos;
using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Common.Interfaces;

public interface ISearchService
{
    Task<SearchResult> SearchAsync(SearchRequest searchRequest);
}
