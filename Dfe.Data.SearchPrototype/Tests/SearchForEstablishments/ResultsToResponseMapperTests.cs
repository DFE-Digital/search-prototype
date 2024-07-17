using Dfe.Data.SearchPrototype.Search;
using Xunit;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments;

public class ResultsToResponseMapperTests
{
    [Fact]
    public void MapFrom_ValidInput_ReturnsCorrectResponse()
    {
        EstablishmentResults input = new EstablishmentResults();
        input.AddEstablishment(new Establishment("urn", "123456"));
    }
}
