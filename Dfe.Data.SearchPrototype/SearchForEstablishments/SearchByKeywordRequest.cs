namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// This is the object used to make requests (send input) through to the
/// T:Dfe.Data.SearchPrototype.SearchForEstablishments.SearchByKeywordUseCase instance.
/// </summary>
public sealed class SearchByKeywordRequest
{
    /// <summary>
    /// The following arguments are passed via the constructor and used to create
    /// an immutable instance of the T:Dfe.Data.SearchPrototype.Search.SearchContext.
    /// </summary>
    /// <param name="searchKeyword">
    /// The string keyword used to search the collection specified.
    /// </param>
    public SearchByKeywordRequest(string searchKeyword)
    {
        Context = SearchRequest.Create(searchKeyword);
    }

    /// <summary>
    /// This property exposes the T:Dfe.Data.SearchPrototype.Search.SearchContext object
    /// which encapsulates the criteria necessary to perform a valid search.
    /// </summary>
    public SearchRequest? Context { get; set; }
}
