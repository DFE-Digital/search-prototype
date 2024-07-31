using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Search;
using Moq;
using System.Linq.Expressions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal static class SearchServiceTestDouble
{
    public static ISearchService DefaultMock() => Mock.Of<ISearchService>();
    public static Expression<Func<ISearchService, Task<Response<SearchResults<Establishment>>>>> SearchRequest(string keyword, string collection) =>
        searchService => searchService.SearchAsync<Establishment>(keyword, collection, It.IsAny<SearchOptions>());

    public static ISearchService MockFor(Task<Response<SearchResults<Establishment>>> searchResult, string keyword, string collection)
    {
        var searchServiceMock = new Mock<ISearchService>();

        searchServiceMock.Setup(SearchRequest(keyword, collection))
            .Returns(searchResult);

        return searchServiceMock.Object;
    }

    public static ISearchService MockSearchService(string keyword, string collection)
    {
        var responseMock = new Mock<Response>();

        var validServiceResponseFake =
            Task.FromResult(
                Response.FromValue(
                    SearchModelFactory.SearchResults(
                        SearchResultFake.SearchResults(), 100, null, null, responseMock.Object), responseMock.Object));

        return MockFor(validServiceResponseFake, keyword, collection);
    }

    public static ISearchService MockForDefaultResult()
    {
        var validServiceResponseFake =
            Task.FromResult<Response<SearchResults<Establishment>>>(default!);

        return MockFor(validServiceResponseFake, string.Empty, string.Empty);
    }
}
