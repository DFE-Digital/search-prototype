using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.TestDoubles;

public static class EstablishmentFacetsTestDouble
{
    public static EstablishmentFacets Create()
    {
        var facets = new List<EstablishmentFacet>(
            new List<EstablishmentFacet>()
            {
                new EstablishmentFacet("name", new List<FacetResult>() { new FacetResult("value1", 1)})
            });
        return new EstablishmentFacets(facets);
    }
}
