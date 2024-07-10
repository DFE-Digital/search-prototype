using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Mapping;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping.TestDoubles;
using Dfe.Data.SearchPrototype.Search.Domain.AggregateRoot.ValueObjects;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using FluentAssertions;
using Xunit;
using static Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping.TestDoubles.ObjectFactoryMapperTestDoubles;
using static Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.SearchServiceTestDouble;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping
{
    public sealed class EstablishmentIdentityMapperTests
    {
        [Fact]
        public void MapFrom_With_Configured_Search_Results_Returns_Configured_EstablishmentIdentifier()
        {
            // arrange
            IObjectFactoryMapper objectFactoryMapper =
                ObjectFactoryMapperTestDouble
                    .MockFor<dynamic, EstablishmentIdentifier>(
                        EstablishmentIdentifierFake.GetEstablishmentIdentifierFake());

            IMapper<SearchResult<object>, EstablishmentIdentifier> mapper =
                new EstablishmentIdentityMapper(objectFactoryMapper);

            const string SearchResultDocument = "{\"name\":\"Test\"}";

            // act

            EstablishmentIdentifier establishmentIdentifier =
                mapper.MapFrom(SearchResultFake.SearchResultFakeWithDocument(SearchResultDocument));

            // assert
            establishmentIdentifier.Should().NotBeNull();
            establishmentIdentifier.URN.Should().NotBeNull().And.NotBeEmpty();
        }

        [Fact]
        public void MapFrom_With_Null_Search_Results_Throws_Expected_Argument_Null_Exception()
        {
            // arrange
            IObjectFactoryMapper objectFactoryMapper = ObjectFactoryMapperTestDouble.Dummy();

            IMapper<SearchResult<object>, EstablishmentIdentifier> mapper =
                new EstablishmentIdentityMapper(objectFactoryMapper);

            // act
            mapper
                .Invoking(mapper =>
                    mapper.MapFrom(input: null!))
                        .Should()
                            .Throw<ArgumentNullException>()
                            .WithMessage("Value cannot be null. (Parameter 'searchResult')");
        }
    }
}