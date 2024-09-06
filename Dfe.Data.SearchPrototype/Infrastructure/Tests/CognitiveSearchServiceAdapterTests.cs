using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests;

public sealed class CognitiveSearchServiceAdapterTests
{
    private static CognitiveSearchServiceAdapter<Establishment> CreateServiceAdapterWith(
        ISearchByKeywordService searchByKeywordService,
        ISearchOptionsFactory searchOptionsFactory,
        IMapper<Pageable<SearchResult<Establishment>>, EstablishmentResults> searchResponseMapper,
        IMapper<Dictionary<string, IList<Azure.Search.Documents.Models.FacetResult>>, EstablishmentFacets> facetsMapper
       ) =>
           new(searchByKeywordService, searchOptionsFactory, searchResponseMapper, facetsMapper);

    [Fact]
    public Task Search_WithNoSearchOptions_ThrowsApplicationException()
    {
        var mockServiceBuilder = new SearchServiceMockBuilder();
        var mockService = mockServiceBuilder.MockSearchService("SearchKeyword", "TargetCollection");
        var mockSearchOptionsFactory = SearchOptionsFactoryTestDouble.MockForNoOptions();
        var mockEstablishmentResultsMapper = PageableSearchResultsToEstablishmentResultsMapperTestDouble.DefaultMock();
        var mockFacetsMapper = AzureFacetResultToEstablishmentFacetsMapperTestDouble.DefaultMock();

        // arrange
        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            CreateServiceAdapterWith(
                mockService,
                mockSearchOptionsFactory,
                mockEstablishmentResultsMapper,
                mockFacetsMapper);

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
    public Task Search_MapperThrowsException_ExceptionPassesThrough()
    {
        // arrange
        var mockServiceBuilder = new SearchServiceMockBuilder();
        var mockService = mockServiceBuilder.MockSearchService("SearchKeyword", "TargetCollection");
        var mockSearchOptionsFactory = SearchOptionsFactoryTestDouble.MockSearchOptionsFactory();
        var mockEstablishmentResultsMapper = PageableSearchResultsToEstablishmentResultsMapperTestDouble.MockMapperThrowingArgumentException();
        var mockFacetsMapper = AzureFacetResultToEstablishmentFacetsMapperTestDouble.DefaultMock();

        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            CreateServiceAdapterWith(
                mockService,
                mockSearchOptionsFactory,
                mockEstablishmentResultsMapper,
                mockFacetsMapper);

        // act, assert.
        return cognitiveSearchServiceAdapter
            .Invoking(adapter => adapter.SearchAsync(new SearchContext(
                            searchKeyword: "SearchKeyword",
                            targetCollection: "TargetCollection")))
            .Should()
            .ThrowAsync< ArgumentException>();
    }
}
