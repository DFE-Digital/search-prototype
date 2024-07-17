using Dfe.Data.SearchPrototype.Search;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

public sealed class SearchByKeywordResponse
{
    public IReadOnlyCollection<Establishment> EstablishmentResults { get;}
    public SearchByKeywordResponse(IReadOnlyCollection<Establishment> establishments)
    {
        EstablishmentResults = establishments;
    }
   
}
