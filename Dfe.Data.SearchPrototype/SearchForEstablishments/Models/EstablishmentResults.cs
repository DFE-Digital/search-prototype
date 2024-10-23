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
    /// The Total Count returned from Establishment search gives us a total
    /// of all available records which correlates with the given search criteria.
    /// </summary>
    public long? TotalNumberOfEstablishments {  get; }

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
    /// <param name="totalNumberOfEstablishments">
    /// The Total Count returned from Establishment search gives us a total
    /// of all available records which correlates with the given search criteria.
    /// </param>
    public EstablishmentResults(IEnumerable<Establishment> establishments, long? totalNumberOfEstablishments)
    {
        _establishments = establishments.ToList();
        TotalNumberOfEstablishments = totalNumberOfEstablishments;
    }
}