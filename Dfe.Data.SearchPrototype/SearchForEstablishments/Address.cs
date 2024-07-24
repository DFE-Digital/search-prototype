namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

public class Address
{
    public string? Street { get; }

    public string? Locality { get; }

    public string? Address3 { get; }

    public string? Town { get; }

    public string? Postcode { get; }
    public Address(string? street, string? locality, string? address3, string? town, string? postcode)
    {
        Street = street;
        Locality = locality;
        Address3 = address3;
        Town = town;
        Postcode = postcode;
    }
}
