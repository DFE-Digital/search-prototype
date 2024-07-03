namespace Dfe.Data.SearchPrototype.Search.Application.Adapters
{
    public sealed class SearchContext
    {
        public SearchContext(string searchKeyword, string searchTarget)
        {
            SearchKeyword =
                (string.IsNullOrWhiteSpace(searchKeyword)) ?
                    throw new ArgumentNullException(nameof(searchKeyword)) : searchKeyword;

            SearchTarget =
                (string.IsNullOrWhiteSpace(searchTarget)) ?
                    throw new ArgumentNullException(nameof(searchTarget)) : searchTarget;
        }

        public string SearchKeyword { get; }
        public string SearchTarget { get; }
    }
}