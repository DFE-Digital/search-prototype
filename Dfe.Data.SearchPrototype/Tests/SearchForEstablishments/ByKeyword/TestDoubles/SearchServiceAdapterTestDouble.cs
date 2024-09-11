using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Moq;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.ByKeyword.TestDoubles;

public static class SearchServiceAdapterTestDouble
{
    public static ISearchServiceAdapter MockFor(SearchResults searchResults)
    {
        Mock<ISearchServiceAdapter> searchServiceAdapter = new();

        searchServiceAdapter
            .Setup(adapter => adapter.SearchAsync(It.IsAny<SearchRequest>()))
            .ReturnsAsync(searchResults);

        return searchServiceAdapter.Object;
    }
}
