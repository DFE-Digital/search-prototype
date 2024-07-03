using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Search.Domain;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mapping
{
    public sealed class AzureResponseToSearchResultsMapper : IMapper<Response<SearchResults<object>>, Establishments>
    {
        public Establishments MapFrom(Response<SearchResults<object>> input)
        {
            return new Establishments(
                new EstablismentsIdentifier(Guid.NewGuid()));
        }
    }
}

//public sealed class SearchResponse
//{
//    public Pageable<SearchResult<object>> RawSearchResults { get; }
//    public IDictionary<string, IList<FacetResult>> FacetsReturned { get; }

//    public SearchResponse(
//        SearchResults<object> rawSearchResults,
//        IDictionary<string, IList<FacetResult>>? facets = null)
//    {
//        RawSearchResults = rawSearchResults.GetResults();
//        FacetsReturned = facets ?? new Dictionary<string, IList<FacetResult>>();
//    }

//    public bool HasSearchResults() => RawSearchResults?.Any() == true;
//}