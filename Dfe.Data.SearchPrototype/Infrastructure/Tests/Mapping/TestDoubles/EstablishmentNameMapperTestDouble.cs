using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping.TestDoubles
{
    internal static class EstablishmentNameMapperTestDouble
    {
        public static IMapper<SearchResult<object>, EstablishmentName> DefaultMock() =>
            Mock.Of<IMapper<SearchResult<object>, EstablishmentName>>();

        public static IMapper<SearchResult<object>, EstablishmentName> MockFor(EstablishmentName establishmentName)
        {
            var mapperMock = new Mock<IMapper<SearchResult<object>, EstablishmentName>>();

            mapperMock.Setup(
                mapper =>
                    mapper.MapFrom(
                        It.IsAny<SearchResult<object>>())).Returns(establishmentName);

            return mapperMock.Object;
        }
    }

    public static class EstablishmentNameFake
    {
        public static EstablishmentName GetEstablishmentNameFake() => new(new Bogus.Faker().Company.CompanyName());
    }
}
