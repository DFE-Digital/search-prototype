using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using DfE.Data.ComponentLibrary.Infrastructure.CognitiveSearch.Search;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles
{
    internal static class SearchOptionsFactoryTestDouble
    {
        public static ISearchOptionsFactory DefaultMock() => Mock.Of<ISearchOptionsFactory>();

        public static ISearchOptionsFactory MockFor(SearchOptions searchOptions)
        {
            var searchOptionsFactoryMock = new Mock<ISearchOptionsFactory>();

            searchOptionsFactoryMock.Setup(
                searchOptionsFactory =>
                    searchOptionsFactory.GetSearchOptions(
                        It.IsAny<string>())).Returns(searchOptions);

            return searchOptionsFactoryMock.Object;
        }

        public static ISearchOptionsFactory MockSearchOptionsFactory()
        {
            SearchOptions searchOptionsFake = new()
            {
                SearchMode = SearchMode.Any,
                Size = 100,
                IncludeTotalCount = true,
                SearchFields = { "ESTABLISHMENTNAME" }
            };

            return MockFor(searchOptionsFake);
        }

        public static ISearchOptionsFactory MockForDefaultResult() => MockFor(default!);
    }
}
