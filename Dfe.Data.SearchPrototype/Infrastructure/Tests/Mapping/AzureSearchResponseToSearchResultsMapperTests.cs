using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Mapping;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping.TestDoubles;
using Dfe.Data.SearchPrototype.Search.Domain.AggregateRoot;
using Dfe.Data.SearchPrototype.Search.Domain.AggregateRoot.ValueObjects;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using FluentAssertions;
using Moq;
using Xunit;
using static Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.SearchServiceTestDouble;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping
{
    public sealed class AzureSearchResponseToSearchResultsMapperTests
    {
        [Fact]
        public void MapFrom_With_Valid_Search_Results_Returns_Configured_Establishments()
        {
            // arrange
            IMapper<SearchResult<object>, EstablishmentIdentifier> identityMapper =
                EstablishmentIdentityMapperTestDoubles.MockFor(EstablishmentIdentifierFake.GetEstablishmentIdentifierFake());
            IMapper<SearchResult<object>, EstablishmentDefinition> nameMapper = 
                EstablishmentNameMapperTestDouble.MockFor(EstablishmentNameFake.GetEstablishmentNameFake());

            IMapper<Response<SearchResults<object>>, Establishments> mapper =
                new AzureSearchResponseToSearchResultsMapper(identityMapper, nameMapper);

            var responseMock = new Mock<Response>();

            // act
            Response<SearchResults<object>> responseFake =
                Response.FromValue(
                    SearchModelFactory.SearchResults(
                        SearchResultFake.SearchResultFakes(), 100, null, null, responseMock.Object), responseMock.Object);

            Establishments? result = mapper.MapFrom(responseFake);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void MapFrom_With_Null_Search_Results_Throws_Expected_Argument_Null_Exception()
        {
            // arrange
            IMapper<SearchResult<object>, EstablishmentIdentifier> identityMapper =
                EstablishmentIdentityMapperTestDoubles.DefaultMock();
            IMapper<SearchResult<object>, EstablishmentDefinition> nameMapper =
                EstablishmentNameMapperTestDouble.DefaultMock();

            IMapper<Response<SearchResults<object>>, Establishments> mapper =
                new AzureSearchResponseToSearchResultsMapper(identityMapper, nameMapper);

            // act
            Response<SearchResults<object>> responseFake = null!;

            // act.
            mapper
                .Invoking(mapper =>
                    mapper.MapFrom(responseFake))
                        .Should()
                            .Throw<ArgumentNullException>()
                            .WithMessage("Value cannot be null. (Parameter 'input')");
        }
    }
}
