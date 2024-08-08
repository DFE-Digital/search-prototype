using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;
using Moq;
using System.Linq.Expressions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal static class SearchServiceTestDouble
{
    public static ISearchByKeywordService DefaultMock() => Mock.Of<ISearchByKeywordService>();
    public static Expression<Func<ISearchByKeywordService, Task<Response<SearchResults<Establishment>>>>> SearchRequest(string keyword, string collection) =>
        searchService => searchService.SearchAsync<Establishment>(keyword, collection, It.IsAny<SearchOptions>());

    public static ISearchByKeywordService MockFor(Task<Response<SearchResults<Establishment>>> searchResult, string keyword, string collection)
    {
        var searchServiceMock = new Mock<ISearchByKeywordService>();

        searchServiceMock.Setup(SearchRequest(keyword, collection))
            .Returns(searchResult);

        return searchServiceMock.Object;
    }

    public static ISearchByKeywordService MockSearchService(string keyword, string collection)
    {
        var responseMock = new Mock<Response>();

        var validServiceResponseFake =
            Task.FromResult(
                Response.FromValue(
                    SearchModelFactory.SearchResults(
                        SearchResultFake.SearchResults(), 100, null, null, responseMock.Object), responseMock.Object));

        return MockFor(validServiceResponseFake, keyword, collection);
    }

    public static ISearchByKeywordService MockForDefaultResult()
    {
        var validServiceResponseFake =
            Task.FromResult<Response<SearchResults<Establishment>>>(default!);

        return MockFor(validServiceResponseFake, It.IsAny<string>(), It.IsAny<string>());
    }
}
