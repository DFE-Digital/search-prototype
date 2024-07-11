using Azure;
using Azure.Search.Documents.Models;
using Bogus;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public static class SearchResultFake
{
    public static SearchResult<object>[] SearchResultFakes()
    {
        var searchResultFake =
           new Faker<FakeSearchResult>()
           .StrictMode(false)
              .RuleFor(
                   searchResult => searchResult.Name,
                   _ => new Bogus.Faker().Company.CompanyName());

        int amount = new Bogus.Faker().Random.Number(1, 10);
        var searchResults = new List<SearchResult<object>>();

        for (int i = 0; i < amount; i++)
        {
            var fakeSearchResult = searchResultFake.Generate();
            searchResults.Add(SearchModelFactory.SearchResult((object)fakeSearchResult, 100, null));
        }

        return searchResults.ToArray();
    }

    public static SearchResult<object> SearchResultFakeWithDocument(string document) =>
        SearchModelFactory
            .SearchResult<object>(
                document, 1.00, new Dictionary<string, IList<string>>());

    public static List<SearchResult<object>> SearchResultsFakeWithDocuments(List<string> documents)
    {
        var searchResults = new List<SearchResult<object>>();
        foreach (var document in documents)
        {
            searchResults.Add(SearchModelFactory
                .SearchResult<object>(
                    document, 1.00, new Dictionary<string, IList<string>>()));
        }
        return searchResults;
    }

    internal class FakeSearchResult
    {
        public string? Name { get; set; }
    }
}

public static class ResponseFake
{
    public static Response<SearchResults<object>> WithSearchResults(IEnumerable<SearchResult<object>> searchResults)
    {
        var responseMock = new Mock<Response>();
        return Response.FromValue(
                SearchModelFactory.SearchResults(
                    searchResults, 100, null, null, responseMock.Object), responseMock.Object);
    }
}