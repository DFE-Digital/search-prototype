namespace Dfe.Data.SearchPrototype.Search;

/// <summary>
/// Object used to encapsulate the aggregation of establishment search results.
/// </summary>
public sealed class EstablishmentResults
{
    private readonly List<Establishment> _establishments = new();

    /// <summary>
    /// The read-only collection of establishments hydrated through the results of the given search.
    /// </summary>
    public IReadOnlyCollection<Establishment> Establishments => _establishments.AsReadOnly();

    /// <summary>
    /// Function to allow a T:Dfe.Data.SearchPrototype.Search.Establishment object to be
    /// added to the internal read-only collection of establishments.
    /// </summary>
    /// <param name="establishment">
    /// The T:Dfe.Data.SearchPrototype.Search.Establishment object to be added.
    /// </param>
    public void AddEstablishment(Establishment establishment)
    {
        ArgumentNullException.ThrowIfNull(establishment);
        _establishments.Add(establishment);
    }
}