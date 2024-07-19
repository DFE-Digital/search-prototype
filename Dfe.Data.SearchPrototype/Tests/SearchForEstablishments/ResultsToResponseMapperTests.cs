using Dfe.Data.SearchPrototype.Search;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.TestDoubles;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
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

        // act.
        IMapper<EstablishmentResults, SearchByKeywordResponse> mapper = new ResultsToResponseMapper();
        SearchByKeywordResponse response =  mapper.MapFrom(input);

        //assert.
        response.Should().NotBeNull();
        response.EstablishmentResults.Should().HaveCountGreaterThanOrEqualTo(1);
        response.EstablishmentResults!.First().Urn.Should().Be(input.Establishments.First().Urn);
        response.EstablishmentResults!.First().Name.Should().Be(input.Establishments.First().Name);
    }
}
