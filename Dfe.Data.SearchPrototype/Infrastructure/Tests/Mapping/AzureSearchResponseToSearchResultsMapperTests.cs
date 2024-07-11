using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Mapping;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping;

public sealed class AzureSearchResponseToSearchResultsMapperTests
{
    [Fact]
    public void MapFrom_With_Valid_Search_Results_Returns_Configured_Establishments()
    {
        // arrange
        IMapper<Response<SearchResults<object>>, EstablishmentResults> mapper =
            new AzureSearchResponseToSearchResultsMapper();

        var SearchResultDocuments = new List<string>() { "{\"id\":\"123456\",\"ESTABLISHMENTNAME\":\"Establishment name\"}" };
        Response<SearchResults<object>> responseFake =
            ResponseFake
                .WithSearchResults(
                    SearchResultFake.SearchResultsFakeWithDocuments(SearchResultDocuments));

        // act
        EstablishmentResults? result = mapper.MapFrom(responseFake);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void MapFrom_With_Null_Search_Results_Throws_Expected_Argument_Null_Exception()
    {
        // arrange
        IMapper<Response<SearchResults<object>>, EstablishmentResults> mapper =
            new AzureSearchResponseToSearchResultsMapper();

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
