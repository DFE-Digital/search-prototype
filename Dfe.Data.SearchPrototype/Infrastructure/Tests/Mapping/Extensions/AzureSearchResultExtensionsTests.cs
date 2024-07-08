using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Mapping.Extensions;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using FluentAssertions;
using System.Dynamic;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping.Extensions
{
    public sealed class AzureSearchResultExtensionsTests
    {
        [Fact]
        public void DeserialiseSearchResultDocument_With_Valid_SearchResult_Object_Returns_Configured_ExpandoObject()
        {
            // arrange
            SearchResult<object> searchResult =
                SearchServiceTestDouble.SearchResultFake.searchResultFake();

            // act
            ExpandoObject deserialisedSearchResult =
                searchResult.DeserialiseSearchResultDocument();

            // assert
            deserialisedSearchResult.Should().NotBeNull();
        }

        [Fact]
        public void DeserialiseSearchResultDocument_With_Null_SearchResult_Object_Throws_Expected_Argument_Null_Exception()
        {
            // arrange
            SearchResult<object> searchResult = null!;

            // act.
            try
            {
                searchResult.DeserialiseSearchResultDocument();
            }
            catch (ArgumentNullException ex)
            {
                ex.Should().BeOfType<ArgumentNullException>();
                ex.Message.Should().Be("Value cannot be null. (Parameter 'searchResult')");
            }
        }
    }
}
