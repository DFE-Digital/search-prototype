namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// Encapsulates the address details of an Establishment
/// </summary>
public class Address
{
    /// <summary>
    /// The first line of the address.
    /// </summary>
    public string? Street { get; init; }
    /// <summary>
    /// The second line of the address.
    /// </summary>
    public string? Locality { get; init; }
    /// <summary>
    /// The third line of the address.
    /// </summary>
    public string? Address3 { get; init; }
    /// <summary>
    /// The fourth line of the address.
    /// </summary>
    public string? Town { get; init; }
    /// <summary>
    /// The postcode
    /// </summary>
    public string? Postcode { get; init; }

    /// <summary>
    /// default constructor
    /// </summary>
    public Address()
    {
    }

    /// <summary>
    /// Establishes an immutable address instance via the constructor arguments specified.
    /// </summary>
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
    public Address(string? street, string? locality, string? address3, string? town, string? postcode)
    {
        Street = street;
        Locality = locality;
        Address3 = address3;
        Town = town;
        Postcode = postcode;
    }
}
