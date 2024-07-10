namespace Dfe.Data.SearchPrototype.Search;

public sealed class EstablishmentResults
{
    private readonly List<Establishment> _establishments = new();

    public IReadOnlyCollection<Establishment> Establishments => _establishments.AsReadOnly();

    public void AddEstablishment(Establishment? establishment)
    {
        ArgumentNullException.ThrowIfNull(establishment);
        _establishments.Add(establishment);
    }
}
