namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// Encapsulates the aggregation of <see cref="Establishment"/>
/// types returned from the underlying search system.
/// </summary>
public sealed class EstablishmentResults
{
    private readonly List<Establishment> _establishments;

    /// <summary>
    /// The readonly collection of <see cref="Establishment"/>
    /// types derived from the underlying search mechanism.
    /// </summary>
    public IReadOnlyCollection<Establishment> Establishments => _establishments.AsReadOnly();

    /// <summary>
    ///  Default constructor initialises a new readonly
    ///  collection of <see cref="Establishment"/> instances.
    /// </summary>
    public EstablishmentResults()
    {
        _establishments = [];
    }

    /// <summary>
    ///  Establishes an immutable collection of <see cref="Establishment"/>
    ///  instance via the constructor argument specified.
    /// </summary>
    /// <param name="establishments">
    /// Collection of configured <see cref="Establishment"/> instances.
    /// </param>
    public EstablishmentResults(IEnumerable<Establishment> establishments)
    {
        _establishments = establishments.ToList();
    }
}