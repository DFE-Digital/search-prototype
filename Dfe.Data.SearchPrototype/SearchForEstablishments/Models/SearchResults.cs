using Dfe.Data.SearchPrototype.Shared.Models;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// Encapsulates the <see cref="EstablishmentResults"/> and <see cref="Shared.Models.Facets"/>
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
    /// The <see cref="Shared.Models.Facets"/> returned from the Establishment search
    /// which encapsulates the underlying <see cref="Facet"/> collection
    /// that is built from the underlying search response.
    /// </summary>
    public Facets? Facets { get; init; }
}
