using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;
using Dfe.Data.SearchPrototype.Infrastructure.DataTransferObjects;
using Moq;
using System.Linq.Expressions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal class SearchServiceMockBuilder
{
    public static ISearchByKeywordService DefaultMock() => Mock.Of<ISearchByKeywordService>();
    public static Expression<Func<ISearchByKeywordService, Task<Response<SearchResults<Establishment>>>>> SearchRequest(string keyword, string collection) =>
        searchService => searchService.SearchAsync<Establishment>(keyword, collection, It.IsAny<SearchOptions>());

    private string _keyword = string.Empty;
    private string _collection = string.Empty;
    private long _count = 100;
    private IEnumerable<SearchResult<Establishment>>? _searchResults;
    private Dictionary<string, IList<FacetResult>>? _facets;
    
    public ISearchByKeywordService MockFor(Response<SearchResults<Establishment>> searchResult, string keyword, string collection)
    {
        var searchServiceMock = new Mock<ISearchByKeywordService>();

        searchServiceMock.Setup(SearchRequest(keyword, collection))
            .Returns(Task.FromResult(searchResult))
            .Verifiable();

        return searchServiceMock.Object;
    }

    public SearchServiceMockBuilder WithSearchKeywordAndCollection(string keyword, string collection)
    {
        _keyword = keyword;
        _collection = collection;
        return this;
    }

    public SearchServiceMockBuilder WithSearchResults(IEnumerable<SearchResult<Establishment>> results)
    {
        _searchResults = results;
        return this;
    }

    public SearchServiceMockBuilder WithFacets(Dictionary<string, IList<FacetResult>> facets)
    {
        _facets = facets;
        return this;
    }

    public ISearchByKeywordService Create()
    {
        var response = new AzureSearchResponseTestDoubleBuilder()
            .WithSearchResults(_searchResults)
            .WithFacets(_facets)
            .Create();
        return MockFor(response, _keyword, _collection);
    }
}
