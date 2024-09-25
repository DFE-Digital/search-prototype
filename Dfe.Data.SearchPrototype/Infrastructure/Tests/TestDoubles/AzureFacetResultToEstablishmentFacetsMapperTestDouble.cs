using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Shared.Models;
using Moq;
using System.Linq.Expressions;
using AzureFacetResult = Azure.Search.Documents.Models.FacetResult;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal static class AzureFacetResultToEstablishmentFacetsMapperTestDouble
{
    public static IMapper<Dictionary<string, IList<AzureFacetResult>>, Facets> DefaultMock() =>
        Mock.Of<IMapper<Dictionary<string, IList<AzureFacetResult>>, Facets>>();

    public static Expression< Func <IMapper<Dictionary<string, IList<AzureFacetResult>>, Facets>, Facets>> MapFrom() =>
        mapper => mapper.MapFrom(It.IsAny<Dictionary<string, IList<AzureFacetResult>>>());

    public static IMapper<Dictionary<string, IList<AzureFacetResult>>, Facets> MockFor(Facets establishments)
    {
        var mapperMock = new Mock<IMapper<Dictionary<string, IList<AzureFacetResult>>, Facets>>();

        mapperMock.Setup(MapFrom()).Returns(establishments);

        return mapperMock.Object;
    }

    public static IMapper<Dictionary<string, IList<AzureFacetResult>>, Facets> MockMapperThrowingArgumentException()
    {
        var mapperMock = new Mock<IMapper<Dictionary<string, IList<AzureFacetResult>>, Facets>>();

        mapperMock.Setup(MapFrom()).Throws(new ArgumentException());

        return mapperMock.Object;
    }
}
