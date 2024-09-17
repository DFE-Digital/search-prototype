using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public static class SearchServiceAdapterRequestTestDouble
{
    public static SearchServiceAdapterRequest WithFilters(Dictionary<string, object[]> filters)
    {
        return new SearchServiceAdapterRequest(
            "searchKeyword",
            new List<string>() { "searchField1", "searchField2"},
            new List<string> { "facet1", "facet2" },
            filters
            );
    }
}
