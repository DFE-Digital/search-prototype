namespace Dfe.Data.SearchPrototype.Search;

/// <summary>
/// Object used to encapsulate the aggregation of establishment search results.
/// </summary>
public sealed class EstablishmentResults
{
    /// <summary>
    /// The collection of establishment search results
    /// </summary>
    public List<Establishment> Establishments { get; init; }

    /// <summary>
    /// Default constuctor
    /// </summary>
    public EstablishmentResults()
    {
        Establishments = new();
    }

    /// <summary>
    /// Constructor with the following parameters
    /// </summary>
    /// <param name="establishments">List of Establishments</param>
    public EstablishmentResults(IEnumerable<Establishment> establishments)
    {
        Establishments = establishments.ToList();
    }
}