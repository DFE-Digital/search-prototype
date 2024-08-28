using Azure.Search.Documents.Models;
using Azure;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public class AzureSearchResponseTestDoubleBuilder
{
    private IEnumerable<SearchResult<Establishment>>? _searchResults;
    private Dictionary<string, IList<FacetResult>>? _facetResults;

    public AzureSearchResponseTestDoubleBuilder WithSearchResults(IEnumerable<SearchResult<Establishment>> searchResults)
    {
        _searchResults = searchResults;
        return this;
    }

    public AzureSearchResponseTestDoubleBuilder WithFacets(Dictionary<string, IList<FacetResult>> facetResults)
    {
        _facetResults = facetResults;
        return this;
    }

    public Response<SearchResults<Establishment>> Create()
    {
        var responseMock = new Mock<Response>();
        return Response.FromValue(
                SearchModelFactory.SearchResults(
                    _searchResults, 100, _facetResults, null, responseMock.Object), responseMock.Object);
    }
}
