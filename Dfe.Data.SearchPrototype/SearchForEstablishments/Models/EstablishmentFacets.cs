namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// Encapsulates the aggregation of facets returned from the underlying search system.
/// </summary>
public class EstablishmentFacets
{
    /// <summary>
    /// The readonly collection of facets derived from the underlying search mechanism.
    /// </summary>
    public IReadOnlyCollection<EstablishmentFacet> Facets => _establishmentsFacets.AsReadOnly();

    private readonly List<EstablishmentFacet> _establishmentsFacets;

    /// <summary>
    ///  Default constructor initialises a new readonly
    ///  collection of <see cref="EstablishmentFacet"/> instances.
    /// </summary>
    public EstablishmentFacets()
    {
        _establishmentsFacets = [];
    }

    /// <summary>
    ///  Establishes an immutable collection of <see cref="EstablishmentFacet"/>
    ///  instance via the constructor argument specified.
    /// </summary>
    /// <param name="establishmentFacets">
    /// Collection of configured <see cref="EstablishmentFacet"/> instances.
    /// </param>
    public EstablishmentFacets(IEnumerable<EstablishmentFacet> establishmentFacets)
    {
        _establishmentsFacets = establishmentFacets.ToList();
    }
}