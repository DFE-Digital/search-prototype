namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// Encapsulates the <see cref="EstablishmentResults"/> and <see cref="EstablishmentFacets"/>
/// types that make up the response from the underlying search system.
/// </summary>
public class SearchResults
{
    /// <summary>
    /// The <see cref="EstablishmentResults"/> returned from the Establishment search
    /// which encapsulates the underlying <see cref="Establishment"/> collection
    /// that is built from the underlying search response.
    /// </summary>
    public EstablishmentResults? Establishments { get; init; }

    /// <summary>
    /// The <see cref="EstablishmentFacets"/> returned from the Establishment search
    /// which encapsulates the underlying <see cref="EstablishmentFacet"/> collection
    /// that is built from the underlying search response.
    /// </summary>
    public EstablishmentFacets? Facets { get; init; }

    /// <summary>
    /// The Total Count returned from Establishment search gives us a total
    /// of all available records which correlates with the given search criteria.
    /// </summary>
    public long? TotalNumberOfEstablishments { get; init; }
}
