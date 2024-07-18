using Dfe.Data.SearchPrototype.Search;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// 
/// </summary>
public sealed class SearchByKeywordResponse
{
    /// <summary>
    /// 
    /// </summary>
    public IReadOnlyCollection<Establishment> EstablishmentResults { get;}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="establishments"></param>
    public SearchByKeywordResponse(IReadOnlyCollection<Establishment> establishments)
    {
        EstablishmentResults = establishments;
    }
}
