using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Infrastructure.Mapping.Extensions;
using Xunit;
using System.Dynamic;
using FluentAssertions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping.Extensions
{
    public sealed class AzureSearchResultExtensionsTests
    {
        [Fact]
        public void DeserialiseSearchResultDocument_With_Valid_SearchResult_Object_Returns_Configured_ExpandoObject()
        {
            // arrange
            var searchResult = SearchServiceTestDouble.SearchResultFake.searchResultFake();

            // act
            ExpandoObject deserialisedSearchResult = searchResult.DeserialiseSearchResultDocument();

            // assert
            deserialisedSearchResult.Should().NotBeNull();
        }
    }
}
