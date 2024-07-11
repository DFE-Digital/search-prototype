using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal static class AzureSearchResponseToSearchResultsMapperTestDouble
{
    public static IMapper<Response<SearchResults<object>>, EstablishmentResults> DefaultMock() =>
        Mock.Of<IMapper<Response<SearchResults<object>>, EstablishmentResults>>();

    public static IMapper<Response<SearchResults<object>>, EstablishmentResults> MockFor(EstablishmentResults establishments)
    {
        var mapperMock = new Mock<IMapper<Response<SearchResults<object>>, EstablishmentResults>>();

        mapperMock.Setup(
            mapper =>
                mapper.MapFrom(
                    It.IsAny<Response<SearchResults<object>>>())).Returns(establishments);

        return mapperMock.Object;
    }

    public static IMapper<Response<SearchResults<object>>, EstablishmentResults> MockDefaultMapper()
    {
        int amount = new Bogus.Faker().Random.Number(1, 10);
        var establishmentResults = new EstablishmentResults();

        for (int i = 0; i < amount; i++)
        {
            Establishment establishmentFake =
                EstablishmentFakes.GetEstablishmentFake();

            establishmentResults.AddEstablishment(establishmentFake);
        }

        return MockFor(establishmentResults);
    }

    internal static class EstablishmentFakes
    {
        public static Establishment GetEstablishmentFake() =>
            new(GetEstablishmentIdentifierFake(), GetEstablishmentNameFake());

        private static string GetEstablishmentNameFake() =>
             new Bogus.Faker().Company.CompanyName();

        private static string GetEstablishmentIdentifierFake() =>
            new Bogus.Faker().Random.Int().ToString();
    }
}
