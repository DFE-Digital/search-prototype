using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Builders;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles
{
    internal static class SearchOptionsBuilderTestDouble
    {
        public static ISearchOptionsBuilder MockFor(SearchOptions searchOptions)
        {
            var mockSearchOptionsBuilder = new Mock<ISearchOptionsBuilder>();

            mockSearchOptionsBuilder.Setup(searchOptionsBuilder =>
                searchOptionsBuilder.WithSize(It.IsAny<int?>())).Returns(mockSearchOptionsBuilder.Object);
            mockSearchOptionsBuilder.Setup(searchOptionsBuilder =>
                searchOptionsBuilder.WithOffset(It.IsAny<int?>())).Returns(mockSearchOptionsBuilder.Object);
            mockSearchOptionsBuilder.Setup(searchOptionsBuilder =>
                searchOptionsBuilder.WithSearchMode(It.IsAny<SearchMode>())).Returns(mockSearchOptionsBuilder.Object);
            mockSearchOptionsBuilder.Setup(searchOptionsBuilder =>
                searchOptionsBuilder.WithIncludeTotalCount(It.IsAny<bool?>())).Returns(mockSearchOptionsBuilder.Object);
            mockSearchOptionsBuilder.Setup(searchOptionsBuilder =>
                searchOptionsBuilder.WithSearchFields(It.IsAny<IList<string>?>())).Returns(mockSearchOptionsBuilder.Object);
            mockSearchOptionsBuilder.Setup(searchOptionsBuilder =>
                searchOptionsBuilder.WithFacets(It.IsAny<IList<string>>())).Returns(mockSearchOptionsBuilder.Object);
            mockSearchOptionsBuilder.Setup(searchOptionsBuilder =>
                searchOptionsBuilder.WithFilters(It.IsAny<IList<FilterRequest>>())).Returns(mockSearchOptionsBuilder.Object);

            mockSearchOptionsBuilder.Setup(searchOptionsBuilder =>
               searchOptionsBuilder.Build()).Returns(searchOptions);

            return mockSearchOptionsBuilder.Object;
        }
    }
}
