using Dfe.Data.SearchPrototype.Search;
using Moq;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.TestDoubles;

public static class SearchServiceAdapterTestDouble
{
    public static ISearchServiceAdapter MockFor(EstablishmentResults establishmentResults)
    {
        Mock<ISearchServiceAdapter> searchServiceAdapter = new();

        searchServiceAdapter
            .Setup(adapter => adapter.SearchAsync(It.IsAny<SearchContext>()))
            .ReturnsAsync(establishmentResults);

        return searchServiceAdapter.Object;
    }
}
