using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Moq;
using System.Linq.Expressions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal static class AzureSearchResponseToSearchResultsMapperTestDouble
{
    public static IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> DefaultMock() =>
        Mock.Of<IMapper<Response<SearchResults<Establishment>>, EstablishmentResults>>();
    public static Expression<Func<IMapper<Response<SearchResults<Establishment>>, EstablishmentResults>, EstablishmentResults>> MapFrom() =>
        mapper => mapper.MapFrom(It.IsAny<Response<SearchResults<Establishment>>>());

    public static IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> MockFor(EstablishmentResults establishments)
    {
        var mapperMock = new Mock<IMapper<Response<SearchResults<Establishment>>, EstablishmentResults>>();

        mapperMock.Setup(MapFrom()).Returns(establishments);

        return mapperMock.Object;
    }

    public static IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> MockDefaultMapper()
    {
        var mockMapper = new Mock<IMapper<Response<SearchResults<Establishment>>, EstablishmentResults>>();
        mockMapper.Setup(mapper => mapper.MapFrom(It.IsAny<Response<SearchResults<Establishment>>>()))
            .Returns(new EstablishmentResults());
        return mockMapper.Object;
    }

    internal static class EstablishmentFakes
    {
        public static Establishment GetEstablishmentFake() =>
            new() { id = GetEstablishmentIdentifierFake(), ESTABLISHMENTNAME = GetEstablishmentNameFake() };

        private static string GetEstablishmentNameFake() =>
             new Bogus.Faker().Company.CompanyName();

        private static string GetEstablishmentIdentifierFake() =>
            new Bogus.Faker().Random.Int().ToString();
    }
}
