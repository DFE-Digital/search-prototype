using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Moq;
using System.Linq.Expressions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal static class PageableSearchResultsToEstablishmentResultsMapperTestDouble
{
    public static IMapper<Pageable<SearchResult<Establishment>>, EstablishmentResults> DefaultMock() =>
        Mock.Of<IMapper<Pageable<SearchResult<Establishment>>, EstablishmentResults>>();

    public static Expression<Func<IMapper<Pageable<SearchResult<Establishment>>, EstablishmentResults>, EstablishmentResults>> MapFrom() =>
        mapper => mapper.MapFrom(It.IsAny<Pageable<SearchResult<Establishment>>>());

    public static IMapper<Pageable<SearchResult<Establishment>>, EstablishmentResults> MockFor(EstablishmentResults establishments)
    {
        var mapperMock = new Mock<IMapper<Pageable<SearchResult<Establishment>>, EstablishmentResults>>();

        mapperMock.Setup(MapFrom()).Returns(establishments);

        return mapperMock.Object;
    }

    public static IMapper<Pageable<SearchResult<Establishment>>, EstablishmentResults> MockMapperThrowingArgumentException()
    {
        var mapperMock = new Mock<IMapper<Pageable<SearchResult<Establishment>>, EstablishmentResults>>();

        mapperMock.Setup(MapFrom()).Throws(new ArgumentException());

        return mapperMock.Object;
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
