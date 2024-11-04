namespace Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;

/// <summary>
/// This is the object used to make requests (send input) through to the
/// <see cref="SearchByKeywordUseCase" /> instance.
/// </summary>
public sealed class SearchByKeywordRequest
{
    /// <summary>
    /// The following search keyword argument is passed via the constructor and used to create
    /// an immutable <see cref="SearchByKeywordRequest" /> instance which encapsulates
    /// the search keyword required to formulate a valid (baseline) search request.
    /// </summary>
    /// <param name="searchKeyword">
    /// The string keyword used to search the collection specified.
    /// </param>
    /// <param name="offset">
    /// The value used to define how many records are skipped in the search
    /// response (if any), by default we choose not to skip any records.
    /// </param> 
    public SearchByKeywordRequest(string searchKeyword, int offset = 0)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(searchKeyword));

        SearchKeyword = searchKeyword;
        Offset = offset;
    }

    /// <summary>
    /// The following search keyword and filter arguments are passed via the constructor and used to create
    /// an immutable <see cref="SearchByKeywordRequest" /> instance which encapsulates the parameters required
    /// to formulate a valid search request which are refined using the provisioned filters.
    /// </summary>
    /// <param name="searchKeyword">
    /// The string keyword used to search the collection specified.
    /// </param>
    /// <param name="filterRequests">
    /// The <see cref="FilterRequest"/> used to refine the search criteria.
    /// </param>
    /// <param name="offset">
    /// The value used to define how many records are skipped in the search response (if any).
    /// </param>
    public SearchByKeywordRequest(
        string searchKeyword,IList<FilterRequest> filterRequests, int offset = 0) : this(searchKeyword, offset)
    {
        FilterRequests = filterRequests;
    }

    /// <summary>
    /// The string keyword used to search the collection specified.
    /// </summary>
    public string SearchKeyword { get; }

    /// <summary>
    /// The value used to define how many records are skipped in the search response (if any).
    /// </summary>
    public int Offset { get; }

    /// <summary>
    /// The filter (key/values) used to refine the search criteria.
    /// </summary>
    public IList<FilterRequest>? FilterRequests { get; }
}
