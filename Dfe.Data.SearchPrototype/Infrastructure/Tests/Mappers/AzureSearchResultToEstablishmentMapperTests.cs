using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping
{
    public sealed class AzureSearchResultToEstablishmentMapperTests
    {
        [Fact]
        public void MapFrom_With_Valid_Search_Result_Returns_Configured_Establishment()
        {
            // arrange
            const string EstablismentId = "123456";
            const string EstablishmentName = "Test Establishment";

            IMapper<Establishment, Search.Establishment> mapper = new AzureSearchResultToEstablishmentMapper();

            var establishmentFake = new Establishment()
            {
                id = EstablismentId,
                ESTABLISHMENTNAME = EstablishmentName
            };

            // act
            Search.Establishment? result = mapper.MapFrom(establishmentFake);

            // assert
            result.Should().NotBeNull();
            result.Urn.Should().Be(EstablismentId);
            result.Name.Should().Be(EstablishmentName);
        }

        [Fact]
        public void MapFrom_With_Null_Search_Result_Throws_Expected_Argument_Null_Exception()
        {
            // arrange.
            IMapper<Establishment, Search.Establishment> mapper = new AzureSearchResultToEstablishmentMapper();

            // act.
            Establishment establishmentFake = null!;

            // act.
            mapper
                .Invoking(mapper =>
                    mapper.MapFrom(establishmentFake))
                        .Should()
                            .Throw<NullReferenceException>();
        }

        [Fact]
        public void MapFrom_With_Null_id_Throws_Expected_Argument_Exception()
        {
            // arrange.
            const string EstablishmentName = "Test Establishment";

            IMapper<Establishment, Search.Establishment> mapper = new AzureSearchResultToEstablishmentMapper();

            // act.
            var establishmentFake = new Establishment()
            {
                id = null!,
                ESTABLISHMENTNAME = EstablishmentName
            };

            // act.
            mapper
                .Invoking(mapper =>
                    mapper.MapFrom(establishmentFake))
                        .Should()
                            .Throw<ArgumentException>();
        }

        [Fact]
        public void MapFrom_With_Null_Name_Throws_Expected_Argument_Exception()
        {
            // arrange.
            const string EstablismentId = "123456";

            IMapper<Establishment, Search.Establishment> mapper = new AzureSearchResultToEstablishmentMapper();

            // act.
            var establishmentFake = new Establishment()
            {
                id = EstablismentId,
                ESTABLISHMENTNAME = null!
            };

            // act.
            mapper
                .Invoking(mapper =>
                    mapper.MapFrom(establishmentFake))
                        .Should()
                            .Throw<ArgumentException>();
        }
    }
}
