using Dfe.Data.SearchPrototype.Search;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments;

public class ResultsToResponseMapperTests
{
    [Fact]
    public void MapFrom_ValidInput_ReturnsCorrectResponse()
    {
        // arrange.
        EstablishmentResults input = new EstablishmentResults();
        input.AddEstablishment(new Establishment("urn", "123456"));

        // act.
        IMapper<EstablishmentResults, SearchByKeywordResponse> mapper = new ResultsToResponseMapper();
        SearchByKeywordResponse response =  mapper.MapFrom(input);

        //assert.
        response.Should().NotBeNull();
        response.EstablishmentResults.Should().HaveCount(1);
        response.EstablishmentResults.First().Urn.Should().Be(input.Establishments.First().Urn);
        response.EstablishmentResults.First().Name.Should().Be(input.Establishments.First().Name);
    }
}
