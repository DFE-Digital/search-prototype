using Dfe.Data.SearchPrototype.Shared.Models;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.ByKeyword.TestDoubles;

public static class EstablishmentFacetsTestDouble
{
    public static Facets Create()
    {
        var facets = new List<Facet>(
            new List<Facet>()
            {
                new Facet("name", new List<FacetValue>() { new FacetValue("value1", 1)})
            });
        return new Facets(facets);
    }
}
