using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Common.Interfaces;

public interface ISearchService
{
    Task<SearchResult> SearchAsync(SearchRequest searchRequest);
}
