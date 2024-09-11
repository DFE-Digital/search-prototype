namespace Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword;

/// <summary>
/// This is the object used to make requests (send input) through to the
/// <see cref="SearchByKeywordUseCase" /> instance.
/// </summary>
public sealed class SearchByKeywordRequest
{
    /// <summary>
    /// The following arguments are passed via the constructor and used to create
    /// an immutable <see cref="SearchByKeywordRequest" /> instance which encapsulates
    /// the parameters required to formulate a valid search request.
    /// </summary>
    /// <param name="searchKeyword">
    /// The string keyword used to search the collection specified.
    /// </param>
    public SearchByKeywordRequest(string searchKeyword)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(searchKeyword));

        SearchKeyword = searchKeyword;
    }

    /// <summary>
    /// The string keyword used to search the collection specified.
    /// </summary>
    public string SearchKeyword { get; }
}
