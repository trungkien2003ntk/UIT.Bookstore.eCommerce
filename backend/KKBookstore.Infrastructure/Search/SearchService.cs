using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models.RequestDtos;
using KKBookstore.Application.Common.Models.ResultDtos;
using Microsoft.Extensions.Options;

namespace KKBookstore.Infrastructure.Search;

public class SearchService : ISearchService
{
    private SearchConfiguration _configration;
    private SearchClient _searchClient;

    public SearchService(
        IOptions<SearchConfiguration> options
    )
    {
        _configration = options.Value;

        var serviceName = _configration.ServiceName;
        var queryApiKey = _configration.QueryApiKey;
        var indexName = "azuresql-product-detail-index";
        Uri serviceEndpoint = new($"https://{serviceName}.search.windows.net/");
        AzureKeyCredential credential = new(queryApiKey);


        _searchClient = new SearchClient(serviceEndpoint, indexName, credential);
    }



    public async Task<SearchResult> SearchAsync(string searchText, SearchOptions options)
    {
        SearchResult mysearchResult = new()
        {
            PageSize = options.Size!.Value,
            TotalPages = 0,
            TotalCount = 0,
            ProductIds = []
        };

        if (!searchText.Contains(' ') && searchText != "*")
            searchText = searchText + "*";


        SearchResults<SearchDocument> searchResult = await _searchClient.SearchAsync<SearchDocument>(searchText, options);

        if (searchResult.SemanticSearch.Answers != null)
        {
            foreach (QueryAnswerResult result in searchResult.SemanticSearch.Answers)
            {
                Console.WriteLine($"Answer Highlights: {result.Highlights}");
                Console.WriteLine($"Answer Text: {result.Text}");
            }
        }

        int count = 0;

        await foreach (SearchResult<SearchDocument> result in searchResult.GetResultsAsync())
        {
            mysearchResult.TotalCount++;
            SearchDocument doc = result.Document;
            var productId = doc["ProductId"].ToString();

            mysearchResult.ProductIds.Add(int.Parse(productId));
        }
        Console.WriteLine($"Total number of search results:{count}");


        mysearchResult.TotalPages = (int)Math.Ceiling((decimal)mysearchResult.TotalCount / mysearchResult.PageSize);

        return mysearchResult;
    }

    public async Task<SearchResult> SearchAsync(SearchRequest searchRequest)
    {
        var searchText = searchRequest.SearchText;
        var options = new SearchOptions
        {
            Size = searchRequest.PageSize,
            Skip = (searchRequest.Page - 1) * searchRequest.PageSize,
            QueryType = SearchQueryType.Semantic,
            IncludeTotalCount = true,
            SearchMode = SearchMode.Any,
            SemanticSearch = new()
            {
                SemanticConfigurationName = "product-detail-semantic",
                QueryCaption = new(QueryCaptionType.Extractive),
                QueryAnswer = new(QueryAnswerType.Extractive)
            }
        };

        return await SearchAsync(searchText, options);
    }
}
