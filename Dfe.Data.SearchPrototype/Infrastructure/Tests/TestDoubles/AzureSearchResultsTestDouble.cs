using Azure;
using Azure.Search.Documents.Models;
using Moq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public class SearchResultFakeBuilder
{
    private List<SearchResult<Establishment>>? _establishmentSearchResults;

    public SearchResultFakeBuilder WithEmptySearchResult()
    {
        _establishmentSearchResults = new List<SearchResult<Establishment>>();
        return this;
    }

    public SearchResultFakeBuilder WithSearchResults()
    {
        int amount = new Bogus.Faker().Random.Number(1, 10);
        var searchResults = new List<SearchResult<Establishment>>();

        for (int i = 0; i < amount; i++)
        {
            searchResults.Add(
                SearchResultWithDocument(
                    EstablishmentTestDouble.Create()
                    ));
        }
        _establishmentSearchResults = searchResults;
        return this;
    }

    public SearchResultFakeBuilder IncludeNullDocument()
    {
        if(_establishmentSearchResults == null){
            _establishmentSearchResults= new List<SearchResult<Establishment>>();
        }
        _establishmentSearchResults.Add(SearchModelFactory
        .SearchResult<Establishment>(
                null!, 1.00, new Dictionary<string, IList<string>>()));
        return this;
    }

    public static SearchResult<Establishment> SearchResultWithDocument(Establishment? document) =>
        SearchModelFactory
            .SearchResult<Establishment>(
                document!, 1.00, new Dictionary<string, IList<string>>());

    public List<SearchResult<Establishment>> Create()
    {
        return _establishmentSearchResults ?? throw new NullReferenceException();
    }
}

public class ResponseFake
{
    private IEnumerable<SearchResult<Establishment>>? _searchResults;
    private Dictionary<string, IList<FacetResult>>? _facetResults;

    public ResponseFake WithSearchResults(IEnumerable<SearchResult<Establishment>> searchResults)
    {
        _searchResults = searchResults;
        return this;
    }

    public ResponseFake WithFacets(Dictionary<string, IList<FacetResult>> facetResults)
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
