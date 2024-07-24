using Azure;
using Azure.Search.Documents.Models;
using Bogus;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public static class SearchResultFake
{
    public static SearchResult<Establishment>[] SearchResultFakes()
    {
        int amount = new Bogus.Faker().Random.Number(1, 10);
        var searchResults = new List<SearchResult<Establishment>>();

        for (int i = 0; i < amount; i++)
        {
            searchResults.Add(
                SearchResultFakeWithDocument(
                    EstablishmentTestDouble.Create()
                    ));
        }

        return searchResults.ToArray();
    }

    public static SearchResult<Establishment> SearchResultFakeWithDocument(Establishment document) =>
        SearchModelFactory
            .SearchResult<Establishment>(
                document, 1.00, new Dictionary<string, IList<string>>());
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
