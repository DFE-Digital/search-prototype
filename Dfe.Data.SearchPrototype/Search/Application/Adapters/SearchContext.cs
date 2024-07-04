namespace Dfe.Data.SearchPrototype.Search.Application.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SearchContext
    {
        /// <summary>
        /// 
        /// </summary>
        public string SearchKeyword { get; }

        /// <summary>
        /// 
        /// </summary>
        public string TargetCollection { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchKeyword"></param>
        /// <param name="targetCollection"></param>
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