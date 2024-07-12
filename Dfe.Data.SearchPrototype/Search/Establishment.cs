namespace Dfe.Data.SearchPrototype.Search;

/// <summary>
/// Object used to encapsulate the establishment search result.
/// </summary>
public class Establishment
{
    /// <summary>
    /// The read-only URN (unique identifier) of the given establishment.
    /// </summary>
    public string Urn { get; }
    /// <summary>
    /// The read-only name associated with the given establishment.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Establishes an immutable establishment instance via the constructor arguments specified.
    /// </summary>
    /// <param name="urn">
    /// The URN (unique identifier) of the given establishment.
    /// </param>
    /// <param name="name">
    /// The name associated with the given establishment.
    /// </param>
    public Establishment(string urn, string name)
    {
        Urn = urn;
        Name = name;
    }
}
