namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// Object used to encapsulate the aggregation of facets returned from search.
/// </summary>
public class EstablishmentFacets
{
    /// <summary>
    /// The readonly collection of facets from the search
    /// </summary>
    public IReadOnlyCollection<EstablishmentFacet> Facets => _establishmentsFacets.AsReadOnly();

    private readonly List<EstablishmentFacet> _establishmentsFacets;

    /// <summary>
    /// Default constuctor
    /// </summary>
    public EstablishmentFacets()
    {
        _establishmentsFacets = new();
    }

    /// <summary>
    /// Constructor with the following parameters
    /// </summary>
    /// <param name="establishmentFacets">List of Establishments</param>
    public EstablishmentFacets(IEnumerable<EstablishmentFacet> establishmentFacets)
    {
        _establishmentsFacets = establishmentFacets.ToList();
    }
}
