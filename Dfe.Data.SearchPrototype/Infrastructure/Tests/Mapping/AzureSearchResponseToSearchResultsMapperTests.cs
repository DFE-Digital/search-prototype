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
        IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> mapper =
            new AzureEstablishmentSearchResponseToSearchResultsMapper();

        var searchResultDocuments = SearchResultFake.SearchResultFakes();
        Response<SearchResults<Establishment>> responseFake =
            ResponseFake
                  .WithSearchResults(searchResultDocuments);

        // act
        EstablishmentResults? result = mapper.MapFrom(responseFake);

        // assert
        // TODO check the actual mappings
        result.Should().NotBeNull();
    }

    [Fact]
    public void MapFrom_With_Null_Search_Results_Throws_Expected_Argument_Null_Exception()
    {
        // arrange
        IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> mapper =
            new AzureEstablishmentSearchResponseToSearchResultsMapper();

        Response<SearchResults<Establishment>> responseFake = null!;

        // act.
        mapper
            .Invoking(mapper =>
                mapper.MapFrom(responseFake))
                    .Should()
                        .Throw<ArgumentNullException>()
                        .WithMessage("Value cannot be null. (Parameter 'input')");
    }
}
