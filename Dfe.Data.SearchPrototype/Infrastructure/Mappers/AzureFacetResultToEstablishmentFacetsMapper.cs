using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Dfe.Data.SearchPrototype.Shared.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

using AzureFacetResult = Azure.Search.Documents.Models.FacetResult;

/// <summary>
/// Maps from an Azure facet result to a collection of <see cref="Facet"/> types.
/// </summary>
public class AzureFacetResultToEstablishmentFacetsMapper : IMapper<Dictionary<string, IList<AzureFacetResult>>, Facets>
{
    /// <summary>
    /// Map from an Azure facet result to a collection of <see cref="Facet"/> types.
    /// </summary>
    /// <param name="facetResult">The Azure facet result</param>
    /// <returns>
    /// A configured <see cref="Facets"/> instance.
    /// </returns>
    public Facets MapFrom(Dictionary<string, IList<AzureFacetResult>> facetResult)
    {
        var establishmentFacets = new List<Facet>();

        foreach (var facetCategory in facetResult.Where(facet => facet.Value != null))
        {
            var values = facetCategory.Value.Select(facetResult =>
                new FacetValue((string)facetResult.Value, facetResult.Count)).ToList();
            
            var establishmentFacet = new Facet(facetCategory.Key, values);

            establishmentFacets.Add(establishmentFacet);
        }
        return new Facets (establishmentFacets);
    }
}
