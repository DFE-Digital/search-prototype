using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.Shared;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public static class SearchServiceAdapterRequestTestDouble
{
    public static SearchServiceAdapterRequest WithFilters(IList<FilterRequest> filters)
    {
        return new SearchServiceAdapterRequest(
            "searchKeyword",
            new List<string>() { "searchField1", "searchField2"},
            new List<string> { "facet1", "facet2" },
            filters
            );
    }
}
