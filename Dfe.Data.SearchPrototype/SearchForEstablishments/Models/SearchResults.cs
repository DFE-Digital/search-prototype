namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// The search results
/// </summary>
public class SearchResults
{
    /// <summary>
    /// The <see cref="EstablishmentResults"/> returned from the Establishment search
    /// </summary>
    public EstablishmentResults? Establishments { get; init; }
    /// <summary>
    /// The <see cref="EstablishmentFacets"/> resturned from the Establishment search
    /// </summary>
    public EstablishmentFacets? Facets { get; init; }
}
