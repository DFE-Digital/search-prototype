using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Search.Domain.AggregateRoot.ValueObjects;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping.TestDoubles
{
    internal static class EstablishmentNameMapperTestDouble
    {
        public static IMapper<SearchResult<object>, EstablishmentDefinition> DefaultMock() =>
            Mock.Of<IMapper<SearchResult<object>, EstablishmentDefinition>>();

        public static IMapper<SearchResult<object>, EstablishmentDefinition> MockFor(EstablishmentDefinition establishmentName)
        {
            var mapperMock = new Mock<IMapper<SearchResult<object>, EstablishmentDefinition>>();

            mapperMock.Setup(
                mapper =>
                    mapper.MapFrom(
                        It.IsAny<SearchResult<object>>())).Returns(establishmentName);

            return mapperMock.Object;
        }
    }

    public static class EstablishmentNameFake
    {
        public static EstablishmentDefinition GetEstablishmentNameFake() => new(new Bogus.Faker().Company.CompanyName());
    }
}
