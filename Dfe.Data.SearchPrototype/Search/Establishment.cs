namespace Dfe.Data.SearchPrototype.Search;

public class Establishment
{
    public string Urn { get; }
    public string Name { get; }

    public Establishment(string urn, string name)
    {
        Urn = urn;
        Name = name;
    }
}
