using Azure.Search.Documents.Models;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Dfe.Data.SearchPrototype.Infrastructure.Mapping;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping.TestDoubles;
using Dfe.Data.SearchPrototype.Search.Domain.AggregateRoot.ValueObjects;
using Xunit;
using static Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping.TestDoubles.ObjectFactoryMapperTestDoubles;
using static Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.SearchServiceTestDouble;
using FluentAssertions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping
{
    public sealed class EstablishmentNameMapperTests
    {
        [Fact]
        public void MapFrom_With_Configured_Search_Results_Returns_Configured_EstablishmentName()
        {
            // arrange
            IObjectFactoryMapper objectFactoryMapper =
                ObjectFactoryMapperTestDouble
                    .MockFor<dynamic, EstablishmentDefinition>(
                        EstablishmentNameFake.GetEstablishmentNameFake());

            IMapper<SearchResult<object>, EstablishmentDefinition> mapper =
                new EstablishmentNameMapper(objectFactoryMapper);

            const string SearchResultDocument = "{\"name\":\"Test\"}";

            // act

            EstablishmentDefinition establishmentDefinition =
                mapper.MapFrom(SearchResultFake.SearchResultFakeWithDocument(SearchResultDocument));

            // assert
            establishmentDefinition.Should().NotBeNull();
            establishmentDefinition.Name.Should().NotBeNull().And.NotBeEmpty();
        }

        [Fact]
        public void MapFrom_With_Null_Search_Results_Throws_Expected_Argument_Null_Exception()
        {
            // arrange
            IObjectFactoryMapper objectFactoryMapper = ObjectFactoryMapperTestDouble.Dummy();

            IMapper<SearchResult<object>, EstablishmentDefinition> mapper =
                new EstablishmentNameMapper(objectFactoryMapper);

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
