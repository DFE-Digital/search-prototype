using Azure;
using Azure.Search.Documents.Models;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public static class SearchResultFake
{
    public static List<SearchResult<Establishment>> EmptySearchResult()
    {
        return new List<SearchResult<Establishment>>();
    }

    public static List<SearchResult<Establishment>> SearchResults()
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
        return searchResults;
    }

    public static SearchResult<Establishment> SearchResultWithDocument(Establishment? document) =>
        SearchModelFactory
            .SearchResult<Establishment>(
                document!, 1.00, new Dictionary<string, IList<string>>());
}

public static class ResponseFake
{
    public static Response<SearchResults<Establishment>> WithSearchResults(IEnumerable<SearchResult<Establishment>> searchResults)
    {
        var responseMock = new Mock<Response>();
        return Response.FromValue(
                SearchModelFactory.SearchResults(
                    searchResults, 100, null, null, responseMock.Object), responseMock.Object);
    }
}
