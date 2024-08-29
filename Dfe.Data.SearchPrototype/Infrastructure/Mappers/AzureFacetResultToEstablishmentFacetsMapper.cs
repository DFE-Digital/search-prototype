using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

using AzureFacetResult = Azure.Search.Documents.Models.FacetResult;

/// <summary>
/// Maps from an Azure facet result to a collection of
/// T:Dfe.Data.SearchPrototype.SearchForEstablishments.Models.EstablishmentFacet
/// </summary>
public class AzureFacetResultToEstablishmentFacetsMapper : IMapper<Dictionary<string, IList<AzureFacetResult>>, EstablishmentFacets>
{
    /// <summary>
    /// Map from an Azure facet result to a collection of
    /// T:Dfe.Data.SearchPrototype.SearchForEstablishments.Models.EstablishmentFacet
    /// </summary>
    /// <param name="facetResult">The Azure facet result</param>
    /// <returns></returns>
    public EstablishmentFacets MapFrom(Dictionary<string, IList<AzureFacetResult>> facetResult)
    {
        if (facetResult == null)
        {
            return new EstablishmentFacets();
        }

        var establishmentFacets = new List<EstablishmentFacet>();

        foreach (var facetCategory in facetResult.Where(facet => facet.Value != null))
        {
            var values = facetCategory.Value.Select(f => new FacetResult((string)f.Value, f.Count)).ToList();
            var establishmentFacet = new EstablishmentFacet(facetCategory.Key, values);

            establishmentFacets.Add(establishmentFacet);
        }

        return new EstablishmentFacets (establishmentFacets);
    }
}
