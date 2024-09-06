using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Moq;
using System.Linq.Expressions;
using AzureFacetResult = Azure.Search.Documents.Models.FacetResult;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal static class AzureFacetResultToEstablishmentFacetsMapperTestDouble
{
    public static IMapper<Dictionary<string, IList<AzureFacetResult>>, EstablishmentFacets> DefaultMock() =>
        Mock.Of<IMapper<Dictionary<string, IList<AzureFacetResult>>, EstablishmentFacets>>();

    public static Expression< Func <IMapper<Dictionary<string, IList<AzureFacetResult>>, EstablishmentFacets>, EstablishmentFacets>> MapFrom() =>
        mapper => mapper.MapFrom(It.IsAny<Dictionary<string, IList<AzureFacetResult>>>());

    public static IMapper<Dictionary<string, IList<AzureFacetResult>>, EstablishmentFacets> MockFor(EstablishmentFacets establishments)
    {
        var mapperMock = new Mock<IMapper<Dictionary<string, IList<AzureFacetResult>>, EstablishmentFacets>>();

        mapperMock.Setup(MapFrom()).Returns(establishments);

        return mapperMock.Object;
    }

    public static IMapper<Dictionary<string, IList<AzureFacetResult>>, EstablishmentFacets> MockMapperThrowingArgumentException()
    {
        var mapperMock = new Mock<IMapper<Dictionary<string, IList<AzureFacetResult>>, EstablishmentFacets>>();

        mapperMock.Setup(MapFrom()).Throws(new ArgumentException());

        return mapperMock.Object;
    }
}
