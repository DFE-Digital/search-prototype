using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using DfE.Data.ComponentLibrary.Infrastructure.CognitiveSearch.Search;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests;

public sealed class CognitiveSearchServiceAdapterTests
{
    private static CognitiveSearchServiceAdapter<Establishment> CreateServiceAdapterWith(
        ISearchService cognitiveSearchService,
        ISearchOptionsFactory searchOptionsFactory,
        IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> searchResponseMapper
       ) =>
           new(cognitiveSearchService, searchOptionsFactory, searchResponseMapper);

    [Fact]
    public async Task Search_With_Valid_SearchContext_Returns_Configured_Results()
    {
        // arrange
        var mockSearchOptionsFactory = SearchOptionsFactoryTestDouble.MockSearchOptionsFactory();
        var mockService = SearchServiceTestDouble.MockSearchService("SearchKeyword", "TargetCollection");
        var mockMapper = AzureSearchResponseToSearchResultsMapperTestDouble.MockDefaultMapper();

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
    public Task Search_With_Valid_SearchContext_No_Options_Returned_Throws_ApplicationException()
    {
        // arrange
        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            CreateServiceAdapterWith(
                SearchServiceTestDouble.MockSearchService("SearchKeyword", "TargetCollection"),
                SearchOptionsFactoryTestDouble.MockForDefaultResult(),
                AzureSearchResponseToSearchResultsMapperTestDouble.MockDefaultMapper());

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
    public Task Search_With_Valid_SearchContext_No_Results_Returned_Throws_ApplicationException()
    {
        // arrange
        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            CreateServiceAdapterWith(
                SearchServiceTestDouble.DefaultMock(),
                SearchOptionsFactoryTestDouble.MockSearchOptionsFactory(),
                AzureSearchResponseToSearchResultsMapperTestDouble.MockDefaultMapper());

        // act.
        return cognitiveSearchServiceAdapter
            .Invoking(async serviceAdapter =>
                await serviceAdapter.SearchAsync(
                    new SearchContext(
                        searchKeyword: "SearchKeyword",
                        targetCollection: "TargetCollection")))
                        .Should()
                            .ThrowAsync<ApplicationException>()
                            .WithMessage("Unable to derive search results based on input SearchKeyword.");
    }
}
