namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

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
    /// The read-only address associated with the given establishment
    /// </summary>
    public Address Address { get; }

    /// <summary>
    /// Establishes an immutable establishment instance via the constructor arguments specified.
    /// </summary>
    /// <param name="urn">
    /// The URN (unique identifier) of the given establishment.
    /// </param>
    /// <param name="name">
    /// The name associated with the given establishment.
    /// </param>
    /// <param name="street">
    /// The first line of the address.
    /// </param>
    /// <param name="locality">
    /// The second line of the address.
    /// </param>
    /// <param name="address3">
    /// The third line of the address.
    /// </param>
    /// <param name="town">
    /// The fourth line of the address.
    /// </param>
    /// <param name="postcode">
    /// The postcode.
    /// </param>
    public Establishment(string urn, string name, Address address)
    {
        Urn = urn;
        Name = name;
        Address = address;
    }
}
