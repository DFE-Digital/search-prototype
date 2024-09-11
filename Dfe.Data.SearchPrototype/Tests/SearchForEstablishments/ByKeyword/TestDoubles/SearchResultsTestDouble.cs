using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.ByKeyword.TestDoubles;

public static class SearchResultsTestDouble
{
    public static SearchResults Create()
    {
        return new SearchResults()
        {
            Facets = EstablishmentFacetsTestDouble.Create(),
            Establishments = EstablishmentResultsTestDouble.Create()
        };
    }

    public static SearchResults CreateWithNoResults()
    {
        return new SearchResults();
    }
}
