using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Moq;
using System.Linq.Expressions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal static class PageableSearchResultsToEstablishmentResultsMapperTestDouble
{
    public static IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults> DefaultMock() =>
        Mock.Of<IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults>>();

    public static Expression<Func<IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults>, EstablishmentResults>> MapFrom() =>
        mapper => mapper.MapFrom(It.IsAny<Pageable<SearchResult<DataTransferObjects.Establishment>>>());

    public static IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults> MockFor(EstablishmentResults establishments)
    {
        var mapperMock = new Mock<IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults>>();

        mapperMock.Setup(MapFrom()).Returns(establishments);

        return mapperMock.Object;
    }

    public static IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults> MockMapperThrowingArgumentException()
    {
        var mapperMock = new Mock<IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults>>();

        mapperMock.Setup(MapFrom()).Throws(new ArgumentException());

        return mapperMock.Object;
    }
}
