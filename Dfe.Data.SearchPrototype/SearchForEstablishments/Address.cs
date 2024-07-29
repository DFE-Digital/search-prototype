namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

public class Address
{
    public string? Street { get; init; }

    public string? Locality { get; init; }

    public string? Address3 { get; init; }

    public string? Town { get; init; }

    public string? Postcode { get; init; }
    
    public Address()
    {
    }
    
    public Address(string? street, string? locality, string? address3, string? town, string? postcode)
    {
        Street = street;
        Locality = locality;
        Address3 = address3;
        Town = town;
        Postcode = postcode;
    }
}
