namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// The object that encapsulates the Faceted results returned by the
/// T:Dfe.Data.SearchPrototype.SearchForEstablishments.SearchByKeywordUseCase instance
/// </summary>
public class EstablishmentFacet
{
    /// <summary>
    /// The facet (field) name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The collection of T:Dfe.Data.SearchPrototype.SearchForEstablishments.Models.FacetResult
    /// </summary>
    public IList<FacetResult> Results { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="facetName"></param>
    /// <param name="facetResults"></param>
    public EstablishmentFacet(string facetName, IList<FacetResult> facetResults)
    {
        Name = facetName;
        Results = facetResults;
    }
}