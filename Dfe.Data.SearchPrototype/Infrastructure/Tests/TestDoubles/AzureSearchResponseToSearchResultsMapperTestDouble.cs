using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Moq;
using System.Linq.Expressions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal static class AzureSearchResponseToSearchResultsMapperTestDouble
{
    public static IMapper<Response<SearchResults<Establishment>>, SearchResults> DefaultMock() =>
        Mock.Of<IMapper<Response<SearchResults<Establishment>>, SearchResults>>();
    public static Expression<Func<IMapper<Response<SearchResults<Establishment>>, SearchResults>, SearchResults>> MapFrom() =>
        mapper => mapper.MapFrom(It.IsAny<Response<SearchResults<Establishment>>>());

    public static IMapper<Response<SearchResults<Establishment>>, SearchResults> MockFor(SearchResults searchResults)
    {
        var mapperMock = new Mock<IMapper<Response<SearchResults<Establishment>>, SearchResults>>();

        mapperMock.Setup(MapFrom()).Returns(searchResults);

        return mapperMock.Object;
    }

    public static IMapper<Response<SearchResults<Establishment>>, SearchResults> MockMapperThrowingArgumentException()
    {
        var mapperMock = new Mock<IMapper<Response<SearchResults<Establishment>>, SearchResults>>();

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
