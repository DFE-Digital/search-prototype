using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Mapping.Extensions;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using FluentAssertions;
using Newtonsoft.Json;
using System.Dynamic;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping.Extensions;

public sealed class AzureSearchResultExtensionsTests
{
    [Fact]
    public void DeserialiseSearchResultDocument_With_Valid_SearchResult_Object_Returns_Configured_ExpandoObject()
    {
        // arrange
        const string SearchResultDocument = "{\"name\":\"Test\"}";
        SearchResult<object> searchResult =
            SearchServiceTestDouble.SearchResultFake
                .SearchResultFakeWithDocument(SearchResultDocument);

        // act
        ExpandoObject? deserialisedSearchResult =
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

    [Fact]
    public void DeserialiseSearchResultDocument_With_Empty_SearchResult_Document_Throws_Expected_Json_Serialization_Exception()
    {
        // arrange
        const string SearchResultDocument = "";
        SearchResult<object> searchResult =
            SearchServiceTestDouble.SearchResultFake
                .SearchResultFakeWithDocument(SearchResultDocument);

        // act.
        try
        {
            searchResult.DeserialiseSearchResultDocument();
        }
        catch (ArgumentException ex)
        {
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("An empty or null search document cannot be serialised.");
        }
    }

    [Fact]
    public void DeserialiseSearchResultDocument_With_Invalid_SearchResult_Object_Throws_Expected_JsonSerialization_Exception()
    {
        // arrange
        const string SearchResultDocument = "{\"name\":\"Test\"";
        SearchResult<object> searchResult =
            SearchServiceTestDouble.SearchResultFake
                .SearchResultFakeWithDocument(SearchResultDocument);

        // act.
        try
        {
            searchResult.DeserialiseSearchResultDocument();
        }
        catch (JsonSerializationException ex)
        {
            ex.Should().BeOfType<JsonSerializationException>();
            ex.Message.Should().Be("Invalid JSON defined in search result document: {\"name\":\"Test\"");
        }
    }
}
