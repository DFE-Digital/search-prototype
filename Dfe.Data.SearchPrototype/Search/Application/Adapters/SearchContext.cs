namespace Dfe.Data.SearchPrototype.Search.Application.Adapters
{
    public sealed class SearchContext
    {
        public SearchContext(string searchKeyword, string targetCollection)
        {
            SearchKeyword =
                (string.IsNullOrWhiteSpace(searchKeyword)) ?
                    throw new ArgumentNullException(nameof(searchKeyword)) : searchKeyword;

            TargetCollection =
                (string.IsNullOrWhiteSpace(targetCollection)) ?
                    throw new ArgumentNullException(nameof(targetCollection)) : targetCollection;
        }

        public string SearchKeyword { get; }
        public string TargetCollection { get; }
    }
}