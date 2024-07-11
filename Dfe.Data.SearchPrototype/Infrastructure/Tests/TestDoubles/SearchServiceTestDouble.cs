using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using DfE.Data.ComponentLibrary.Infrastructure.CognitiveSearch.Search;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal static class SearchServiceTestDouble
{
    public static ISearchService DefaultMock() => Mock.Of<ISearchService>();

    public static ISearchService MockFor(Task<Response<SearchResults<Establishment>>> searchResult)
    {
        var searchServiceMock = new Mock<ISearchService>();

        searchServiceMock.Setup(searchService =>
            searchService
                .SearchAsync<Establishment>(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOptions>()))
                    .Returns(searchResult);

        return searchServiceMock.Object;
    }

    public static ISearchService MockSearchService()
    {
        var responseMock = new Mock<Response>();

        var validServiceResponseFake =
            Task.FromResult(
                Response.FromValue(
                    SearchModelFactory.SearchResults(
                        SearchResultFake.SearchResultFakes(), 100, null, null, responseMock.Object), responseMock.Object));

        return MockFor(validServiceResponseFake);
    }

    public static ISearchService MockForDefaultResult()
    {
        var validServiceResponseFake =
            Task.FromResult<Response<SearchResults<Establishment>>>(default!);

        return MockFor(validServiceResponseFake);
    }
}
