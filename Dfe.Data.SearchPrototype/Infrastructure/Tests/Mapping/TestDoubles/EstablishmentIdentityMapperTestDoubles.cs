using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Search.Domain.AggregateRoot.ValueObjects;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping.TestDoubles
{
    internal static class EstablishmentIdentityMapperTestDoubles
    {
        public static IMapper<SearchResult<object>, EstablishmentIdentifier> DefaultMock() =>
            Mock.Of<IMapper<SearchResult<object>, EstablishmentIdentifier>>();

        public static IMapper<SearchResult<object>, EstablishmentIdentifier> MockFor(EstablishmentIdentifier establishmentIdentifier)
        {
            var mapperMock = new Mock<IMapper<SearchResult<object>, EstablishmentIdentifier>>();

            mapperMock.Setup(
                mapper =>
                    mapper.MapFrom(
                        It.IsAny<SearchResult<object>>())).Returns(establishmentIdentifier);

            return mapperMock.Object;
        }
    }

    public static class EstablishmentIdentifierFake
    {
        public static EstablishmentIdentifier GetEstablishmentIdentifierFake() => new(Guid.NewGuid().ToString());
    }
}
