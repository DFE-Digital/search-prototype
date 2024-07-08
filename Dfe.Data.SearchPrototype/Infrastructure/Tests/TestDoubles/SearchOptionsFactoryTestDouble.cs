using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
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

        public static ISearchOptionsFactory MockSearchOptionsFactory() => MockFor(SearchOptionsFake);

        public static ISearchOptionsFactory MockForDefaultResult() => MockFor(default!);
        public static SearchOptions SearchOptionsFake => new()
        {
            SearchMode = SearchMode.Any,
            Size = 100,
            IncludeTotalCount = true,
            SearchFields = { "ESTABLISHMENTNAME" }
        };
    }
}
