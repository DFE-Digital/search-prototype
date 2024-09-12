using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.ByKeyword.TestDoubles;

public static class SearchByKeywordCriteriaTestDouble
{
    public static SearchByKeywordCriteria Create()
    {
        return new SearchByKeywordCriteria()
        {
            Facets = ["FIELD1", "FIELD2", "FIELD3"],
            SearchFields = ["FACET1", "FACET2", "FACET3"]
        };
    }
}
