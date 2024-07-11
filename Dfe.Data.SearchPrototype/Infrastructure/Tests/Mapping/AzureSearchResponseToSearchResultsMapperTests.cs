using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Mapping;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using FluentAssertions;
using Moq;
using Xunit;
using static Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.SearchServiceTestDouble;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping;

public sealed class AzureSearchResponseToSearchResultsMapperTests
{
    [Fact]
    public void MapFrom_With_Valid_Search_Results_Returns_Configured_Establishments()
    {
        // arrange
        IMapper<Response<SearchResults<object>>, EstablishmentResults> mapper =
            new AzureSearchResponseToSearchResultsMapper();

        var responseMock = new Mock<Response>();

        // act
        const string SearchResultDocument = "{\"id\":\"123456\",\"ESTABLISHMENTNAME\":\"Etablishment name\"}";
        SearchResult<object> searchResult =
            SearchServiceTestDouble.SearchResultFake
                .SearchResultFakeWithDocument(SearchResultDocument);
        Response<SearchResults<object>> responseFake =
            Response.FromValue(
                SearchModelFactory.SearchResults(
                    new List<SearchResult<object>>() { searchResult }, 100, null, null, responseMock.Object), responseMock.Object);

        EstablishmentResults ? result = mapper.MapFrom(responseFake);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void MapFrom_With_Null_Search_Results_Throws_Expected_Argument_Null_Exception()
    {
        // arrange
        IMapper<Response<SearchResults<object>>, EstablishmentResults> mapper =
            new AzureSearchResponseToSearchResultsMapper();

        // act
        Response<SearchResults<object>> responseFake = null!;

        // act.
        mapper
            .Invoking(mapper =>
                mapper.MapFrom(responseFake))
                    .Should()
                        .Throw<ArgumentNullException>()
                        .WithMessage("Value cannot be null. (Parameter 'input')");
    }
}
