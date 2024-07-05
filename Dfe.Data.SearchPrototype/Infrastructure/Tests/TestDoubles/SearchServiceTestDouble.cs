using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Bogus;
using DfE.Data.ComponentLibrary.Infrastructure.CognitiveSearch.Search;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles
{
    internal static class SearchServiceTestDouble
    {
        public static ISearchService Dummy() => Mock.Of<ISearchService>();

        public static ISearchService MockFor(Task<Response<SearchResults<object>>> searchResult)
        {
            var searchServiceMock = new Mock<ISearchService>();

            searchServiceMock.Setup(searchService =>
                searchService
                    .SearchAsync<object>(
                        It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOptions>()))
                        .Returns(searchResult);

            return searchServiceMock.Object;
        }

        public static ISearchService MockFor()
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
                Task.FromResult<Response<SearchResults<object>>>(default!);

            return MockFor(validServiceResponseFake);
        }

        internal static class SearchResultFake
        {
            public static SearchResult<object>[] SearchResultFakes()
            {
                var searchResultFake =
                   new Faker<FakeSearchResult>()
                   .StrictMode(false)
                      .RuleFor(
                           searchResult => searchResult.Name,
                           _ => new Bogus.Faker().Company.CompanyName());

                int amount = new Bogus.Faker().Random.Number(1, 10);
                var searchResults = new List<SearchResult<object>>();

                for (int i = 0; i < amount; i++)
                {
                    var fakeSearchResult = searchResultFake.Generate();
                    searchResults.Add(SearchModelFactory.SearchResult((object)fakeSearchResult, 100, null));
                }

                return searchResults.ToArray();
            }

            internal class FakeSearchResult
            {
                public string? Name { get; set; }
            }
        }
    }
}
