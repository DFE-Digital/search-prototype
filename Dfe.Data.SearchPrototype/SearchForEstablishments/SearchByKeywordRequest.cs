using Dfe.Data.SearchPrototype.Search;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// This is the object used to make requests (send input) through to the
/// T:Dfe.Data.SearchPrototype.SearchForEstablishments.SearchByKeywordUseCase instance.
/// </summary>
public sealed class SearchByKeywordRequest
{
    public SearchByKeywordRequest(string searchKeyword, string targetCollection)
    {
        Context = SearchContext.Create(searchKeyword, targetCollection);   
    }
    /// <summary>
    /// This property exposes the T:Dfe.Data.SearchPrototype.Search.SearchContext object
    /// which encapsulates the criteria necessary to perform a valid search.
    /// </summary>
    public SearchContext? Context { get; set; }
}
