using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests;

public sealed class CognitiveSearchServiceAdapterTests
{
    private static CognitiveSearchServiceAdapter<Establishment> CreateServiceAdapterWith(
        ISearchByKeywordService searchByKeywordService,
        ISearchOptionsFactory searchOptionsFactory,
        IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> searchResponseMapper
       ) =>
           new(searchByKeywordService, searchOptionsFactory, searchResponseMapper);

    [Fact]
    public async Task Search_WithValidSearchContext_ReturnsConfiguredResults()
    {
        // arrange
        var mockService = SearchServiceTestDouble.MockSearchService("SearchKeyword", "TargetCollection");
        var mockSearchOptionsFactory = SearchOptionsFactoryTestDouble.MockSearchOptionsFactory();
        var mockMapper = AzureSearchResponseToSearchResultsMapperTestDouble.MockMapperReturningEmptyResults();

        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            CreateServiceAdapterWith(
                mockService,
                mockSearchOptionsFactory,
                mockMapper);

        // act
        EstablishmentResults? response =
            await cognitiveSearchServiceAdapter.SearchAsync(
                new SearchContext(
                    searchKeyword: "SearchKeyword",
                    targetCollection: "TargetCollection"));

        // assert
        response.Establishments.Should().NotBeNull();
        Mock.Get(mockService).Verify(SearchServiceTestDouble.SearchRequest("SearchKeyword", "TargetCollection"),Times.Once());
        Mock.Get(mockSearchOptionsFactory).Verify(SearchOptionsFactoryTestDouble.SearchOption(), Times.Once());
        Mock.Get(mockMapper).Verify(AzureSearchResponseToSearchResultsMapperTestDouble.MapFrom(), Times.Once());
    }

    [Fact]
    public Task Search_WithNoSearchOptions_ThrowsApplicationException()
    {
        var mockService = SearchServiceTestDouble.MockSearchService("SearchKeyword", "TargetCollection");
        var mockSearchOptionsFactory = SearchOptionsFactoryTestDouble.MockForNoOptions();
        var mockMapper = AzureSearchResponseToSearchResultsMapperTestDouble.MockMapperReturningEmptyResults();

        // arrange
        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            CreateServiceAdapterWith(
                mockService,
                mockSearchOptionsFactory,
                mockMapper);

        // act.
        return cognitiveSearchServiceAdapter
            .Invoking(async serviceAdapter =>
                await serviceAdapter.SearchAsync(
                    new SearchContext(
                        searchKeyword: "SearchKeyword",
                        targetCollection: "TargetCollection")))
            .Should()
            .ThrowAsync<ApplicationException>()
            .WithMessage("Search options cannot be derived for TargetCollection.");
    }

    [Fact]
    public async Task Search_WithNoResultsReturned_ReturnsEmptyResults()
    {
        // arrange
        var mockService = SearchServiceTestDouble.MockSearchService("SearchKeyword", "TargetCollection");
        var mockSearchOptionsFactory = SearchOptionsFactoryTestDouble.MockSearchOptionsFactory();
        var mockMapper = AzureSearchResponseToSearchResultsMapperTestDouble.MockMapperReturningEmptyResults();

        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            CreateServiceAdapterWith(
                mockService,
                mockSearchOptionsFactory,
                mockMapper);

        // act.
        var response = await cognitiveSearchServiceAdapter.SearchAsync(new SearchContext(
                            searchKeyword: "SearchKeyword",
                            targetCollection: "TargetCollection"));

        // assert
        response.Establishments.Should().BeEmpty();
    }
}
