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
    public SearchByKeywordRequest(string searchKeyword)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(searchKeyword));

        SearchKeyword = searchKeyword;
    }

    /// <summary>
    /// The following search keyword and filter arguments are passed via the constructor and used to create
    /// an immutable <see cref="SearchByKeywordRequest" /> instance which encapsulates the parameters required
    /// to formulate a valid search request which are refined using the provisioned filters.
    /// </summary>
    /// <param name="searchKeyword">
    /// The string keyword used to search the collection specified.
    /// </param>
    /// <param name="filters">
    /// The filter (key/values) used to refine the search criteria.
    /// </param>
    public SearchByKeywordRequest(string searchKeyword, IList<KeyValuePair<string, IList<object>>> filters) : this(searchKeyword)
    {
        FilterRequests = filters.ToList().Select(filter => new FilterRequest(filter.Key, filter.Value)).ToList();
    }

    /// <summary>
    /// The string keyword used to search the collection specified.
    /// </summary>
    public string SearchKeyword { get; }

    /// <summary>
    /// The filter (key/values) used to refine the search criteria.
    /// </summary>
    public IList<FilterRequest>? FilterRequests { get; }
}
