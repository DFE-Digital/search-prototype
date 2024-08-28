namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// Object used to encapsulate the aggregation of establishment search results.
/// </summary>
public sealed class EstablishmentResults
{
    /// <summary>
    /// The readonly collection of establishment search results
    /// </summary>
    public IReadOnlyCollection<Establishment> Establishments => _establishments.AsReadOnly();

    /// <summary>
    /// The readonly dictionary of facet results keyed to the faceted field
    /// </summary>
    public IReadOnlyDictionary<string, List<FacetResult>>? Facets => _facets?.AsReadOnly() ?? null;

    private readonly List<Establishment> _establishments;

    private readonly Dictionary<string, List<FacetResult>>? _facets;

    /// <summary>
    /// Default constuctor
    /// </summary>
    public EstablishmentResults()
    {
        _establishments = new();
    }

    /// <summary>
    /// Constructor with the following parameters
    /// </summary>
    /// <param name="establishments">List of Establishments</param>
    /// <param name="facetResults">Dictionary of facets results returned</param>
    public EstablishmentResults(IEnumerable<Establishment> establishments, Dictionary<string, List<FacetResult>>? facetResults = null)
    {
        _establishments = establishments.ToList();
        _facets = facetResults;
    }
}