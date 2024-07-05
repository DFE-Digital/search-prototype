using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Search.Application.Adapters;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using DfE.Data.ComponentLibrary.Infrastructure.CognitiveSearch.Search;
using Xunit;
using FluentAssertions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests
{
    public sealed class CognitiveSearchServiceAdapterTests
    {
        [Fact]
        public async Task MethodName_With_Valid_SearchContext_Returns_Configured_Results()
        {
            // arrange
            ISearchService cognitiveSearchService = SearchServiceTestDouble.MockSearchService();
            ISearchOptionsFactory searchOptionsFactory = SearchOptionsFactoryTestDouble.MockSearchOptionsFactory();
            IMapper<Response<SearchResults<object>>, Establishments> _searchResponseMapper =
                AzureSearchResponseToSearchResultsMapperTestDouble.MockDefaultMapper();

            // act
            ISearchServiceAdapter cognitiveSearchServiceAdapter =
                new CognitiveSearchServiceAdapter(
                    cognitiveSearchService, searchOptionsFactory, _searchResponseMapper);

            Establishments? establishmentResults =
                await cognitiveSearchServiceAdapter.Search(
                    new SearchContext(
                        searchKeyword: "SearchKeyword",
                        targetCollection: "TargetCollection"));

            // assert
            establishmentResults.Should().NotBeNull();
        }
    }
}
