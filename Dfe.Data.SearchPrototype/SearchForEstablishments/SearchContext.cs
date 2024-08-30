namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// Prescribes the context of the search including the keyword and collection target.
/// </summary>
public sealed class SearchContext
{
    /// <summary>
    /// The search keyword(s) to be applied.
    /// </summary>
    public string SearchKeyword { get; }

    /// <summary>
    /// The facets to be returned
    /// </summary>
    public IList<string>? Facets { get; }

    /// <summary>
    /// The target collection on which to apply the search.
    /// </summary>
    public string TargetCollection { get; }

    /// <summary>
    /// The following arguments are passed via the constructor and are not changeable
    /// once an instance is created, this ensures we preserve immutability.
    /// </summary>
    /// <param name="searchKeyword">
    /// The search keyword(s) to be applied.
    /// </param>
    /// <param name="targetCollection">
    /// The target collection on which to apply the search.
    /// </param>
    /// <param name="facets">
    /// The facets to be returned.
    /// </param>
    /// <exception cref="ArgumentNullException"></exception>
    public SearchContext(string searchKeyword, string targetCollection, IList<string>? facets = null)
    {
        SearchKeyword =
            string.IsNullOrWhiteSpace(searchKeyword) ?
                throw new ArgumentNullException(nameof(searchKeyword)) : searchKeyword;

        TargetCollection =
            string.IsNullOrWhiteSpace(targetCollection) ?
                throw new ArgumentNullException(nameof(targetCollection)) : targetCollection;

        Facets = facets;
    }

    /// <summary>
    /// Factory method to allow implicit creation of a T:Dfe.Data.SearchPrototype.Search.SearchContext instance.
    /// </summary>
    /// <param name="searchKeyword">
    /// The keyword string which defines the search.
    /// </param>
    /// <param name="targetCollection">
    /// The underlying collection on which to undertake the search.
    /// </param>
    /// <param name="facets">
    /// The facets to be returned.
    /// </param>
    /// <returns>
    /// A configured T:Dfe.Data.SearchPrototype.Search.SearchContext instance.
    /// </returns>
    public static SearchContext Create(string searchKeyword, string targetCollection, IList<string>? facets = null) => new(searchKeyword, targetCollection, facets);
}
