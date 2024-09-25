namespace Dfe.Data.SearchPrototype.Shared.Models;

/// <summary>
/// Encapsulates the aggregation of facets returned from the underlying search system.
/// </summary>
public class Facets
{
    /// <summary>
    /// The readonly collection of facets derived from the underlying search mechanism.
    /// </summary>
    public IReadOnlyCollection<Facet> FacetCollection => _establishmentsFacets.AsReadOnly();

    private readonly List<Facet> _establishmentsFacets;

    /// <summary>
    ///  Default constructor initialises a new readonly
    ///  collection of <see cref="Facet"/> instances.
    /// </summary>
    public Facets()
    {
        _establishmentsFacets = [];
    }

    /// <summary>
    ///  Establishes an immutable collection of <see cref="Facet"/>
    ///  instance via the constructor argument specified.
    /// </summary>
    /// <param name="establishmentFacets">
    /// Collection of configured <see cref="Facet"/> instances.
    /// </param>
    public Facets(IEnumerable<Facet> establishmentFacets)
    {
        _establishmentsFacets = establishmentFacets.ToList();
    }
}