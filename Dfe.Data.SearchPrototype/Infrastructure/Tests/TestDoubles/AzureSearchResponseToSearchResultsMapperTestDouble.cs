using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles
{
    internal static class AzureSearchResponseToSearchResultsMapperTestDouble
    {
        public static IMapper<Response<SearchResults<object>>, Establishments> DefaultMock() =>
            Mock.Of<IMapper<Response<SearchResults<object>>, Establishments>>();

        public static IMapper<Response<SearchResults<object>>, Establishments> MockFor(Establishments establishments)
        {
            var mapperMock = new Mock<IMapper<Response<SearchResults<object>>, Establishments>>();

            mapperMock.Setup(
                mapper =>
                    mapper.MapFrom(
                        It.IsAny<Response<SearchResults<object>>>())).Returns(establishments);

            return mapperMock.Object;
        }

        public static IMapper<Response<SearchResults<object>>, Establishments> MockDefaultMapper()
        {
            int amount = new Bogus.Faker().Random.Number(1, 10);
            var establishments = Establishments.Create();

            for (int i = 0; i < amount; i++)
            {
                Establishment establishmentFake =
                    EstablishmentFakes.GetEstablishmentFake();

                establishments.AddEstablismentResult(establishmentFake);
            }

            return MockFor(establishments);
        }

        internal static class EstablishmentFakes
        {
            public static Establishment GetEstablishmentFake() =>
                new(GetEstablishmentIdentifierFake(), GetEstablishmentNameFake());

            private static EstablishmentDefinition GetEstablishmentNameFake() =>
                 new EstablishmentDefinition(new Bogus.Faker().Company.CompanyName());

            private static EstablishmentIdentifier GetEstablishmentIdentifierFake() => new(new Bogus.Faker().Random.Int().ToString());
        }
    }
}
