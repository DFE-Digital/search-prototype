namespace Dfe.Data.SearchPrototype.Search
{
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
        /// The target collection on which to apply the search.
        /// </summary>
        public string TargetCollection { get; }

        /// <summary>
        /// </summary>
        /// <param name="searchKeyword">
        /// The search keyword(s) to be applied.
        /// </param>
        /// <param name="targetCollection">
        /// The target collection on which to apply the search.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public SearchContext(string searchKeyword, string targetCollection)
        {
            SearchKeyword =
                (string.IsNullOrWhiteSpace(searchKeyword)) ?
                    throw new ArgumentNullException(nameof(searchKeyword)) : searchKeyword;

            TargetCollection =
                (string.IsNullOrWhiteSpace(targetCollection)) ?
                    throw new ArgumentNullException(nameof(targetCollection)) : targetCollection;
        }
    }
}
