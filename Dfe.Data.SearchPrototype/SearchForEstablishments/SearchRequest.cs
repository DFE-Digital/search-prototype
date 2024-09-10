namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// Prescribes the context of the search including the keyword and collection target.
/// </summary>
public sealed class SearchRequest
{
    /// <summary>
    /// The search keyword(s) to be applied.
    /// </summary>
    public string SearchKeyword { get; }

    /// <summary>
    /// The following arguments are passed via the constructor and are not changeable
    /// once an instance is created, this ensures we preserve immutability.
    /// </summary>
    /// <param name="searchKeyword">
    /// The search keyword(s) to be applied.
    /// </param>
    /// <exception cref="ArgumentNullException"></exception>
    public SearchRequest(string searchKeyword)
    {
        SearchKeyword =
            string.IsNullOrWhiteSpace(searchKeyword) ?
                throw new ArgumentNullException(nameof(searchKeyword)) : searchKeyword;
    }

    /// <summary>
    /// Factory method to allow implicit creation of a T:Dfe.Data.SearchPrototype.Search.SearchContext instance.
    /// </summary>
    /// <param name="searchKeyword">
    /// The keyword string which defines the search.
    /// </param>
    /// <returns>
    /// A configured T:Dfe.Data.SearchPrototype.Search.SearchContext instance.
    /// </returns>
    public static SearchRequest Create(string searchKeyword) => new(searchKeyword);
}
