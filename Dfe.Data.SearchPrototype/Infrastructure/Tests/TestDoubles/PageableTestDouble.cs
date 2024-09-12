using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.DataTransferObjects;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public static class PageableTestDouble
{
    public static Pageable<SearchResult<Establishment>> FromResults(List<SearchResult<Establishment>> results)
    {
        var page = Page<SearchResult<Establishment>>.FromValues(results, continuationToken: null, new Mock<Response>().Object);
        
        return Pageable<SearchResult<Establishment>>.FromPages(new List<Page<SearchResult<Establishment>>>() { page});
    }
}
