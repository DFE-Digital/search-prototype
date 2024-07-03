using Azure;
using Azure.Search.Documents.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options
{
    public sealed class SearchResponse
    {
        public Pageable<SearchResult<object>> RawSearchResults { get; }
        public IDictionary<string, IList<FacetResult>> FacetsReturned { get; }

        public SearchResponse(
            SearchResults<object> rawSearchResults,
            IDictionary<string, IList<FacetResult>>? facets = null)
        {
            RawSearchResults = rawSearchResults.GetResults();
            FacetsReturned = facets ?? new Dictionary<string, IList<FacetResult>>();
        }

        public bool HasSearchResults() => RawSearchResults?.Any() == true;
    }
}
