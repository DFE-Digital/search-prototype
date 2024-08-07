using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.TestDoubles;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments;

public sealed class ResultsToResponseMapperTests
{
    [Fact]
    public void MapFrom_ValidInput_ReturnsCorrectResponse()
    {
        // arrange.
        EstablishmentResults input = EstablishmentResultsTestDouble.Create();
        IMapper<EstablishmentResults, SearchByKeywordResponse> mapper = new ResultsToResponseMapper();

        // act.
        SearchByKeywordResponse response =  mapper.MapFrom(input);

        //assert.
        response.Should().NotBeNull();
        response.Status.Should().Be(SearchResponseStatus.Success);
        response.EstablishmentResults.Should().HaveCountGreaterThanOrEqualTo(1);
        response.EstablishmentResults!.First().Urn.Should().Be(input.Establishments.First().Urn);
        response.EstablishmentResults!.First().Name.Should().Be(input.Establishments.First().Name);
    }

    [Fact]
    public void MapFrom_NullInput_ReturnsErrorResponse()
    {
        // arrange.
        IMapper<EstablishmentResults, SearchByKeywordResponse> mapper = new ResultsToResponseMapper();

        // act
        SearchByKeywordResponse response = mapper.MapFrom(null!);

        // assert
        response.Status.Should().Be(SearchResponseStatus.SearchServiceError);
    }
}
