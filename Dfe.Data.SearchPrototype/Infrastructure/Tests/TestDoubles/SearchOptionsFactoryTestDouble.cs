using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Moq;
using System.Linq.Expressions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal static class SearchOptionsFactoryTestDouble
{
    public static ISearchOptionsFactory DefaultMock() => Mock.Of<ISearchOptionsFactory>();
    public static Expression<Func<ISearchOptionsFactory, SearchOptions?>> SearchOption() =>
        searchOptionsFactory => searchOptionsFactory.GetSearchOptions(It.IsAny<string>());

    public static ISearchOptionsFactory MockFor(SearchOptions searchOptions)
    {
        var searchOptionsFactoryMock = new Mock<ISearchOptionsFactory>();

        searchOptionsFactoryMock.Setup(SearchOption()).Returns(searchOptions);

        return searchOptionsFactoryMock.Object;
    }

    public static ISearchOptionsFactory MockSearchOptionsFactory() => MockFor(SearchOptionsFake);

    public static ISearchOptionsFactory MockForNoOptions() => MockFor(default!);
    public static SearchOptions SearchOptionsFake => new()
    {
        SearchMode = SearchMode.Any,
        Size = 100,
        IncludeTotalCount = true,
        SearchFields = { "ESTABLISHMENTNAME" }
    };
}
