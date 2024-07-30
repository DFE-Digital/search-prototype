namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// Object used to encapsulate the aggregation of establishment search results.
/// </summary>
public sealed class EstablishmentResults
{
    /// <summary>
    /// The readonly collection of establishment search results
    /// </summary>
    public IReadOnlyCollection<Establishment> Establishments => _establishments.AsReadOnly();

    private readonly List<Establishment> _establishments;

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
    public EstablishmentResults(IEnumerable<Establishment> establishments)
    {
        _establishments = establishments.ToList();
    }
}