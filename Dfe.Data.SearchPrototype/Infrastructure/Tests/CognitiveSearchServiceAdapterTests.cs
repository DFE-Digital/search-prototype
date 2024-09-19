using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Filtering;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.Shared;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests;

public sealed class CognitiveSearchServiceAdapterTests
{
    private IMapper<Dictionary<string, IList<Azure.Search.Documents.Models.FacetResult>>, EstablishmentFacets> _mockFacetsMapper
        = AzureFacetResultToEstablishmentFacetsMapperTestDouble.DefaultMock();
    private AzureSearchOptions _options 
        = AzureSearchOptionsTestDouble.Stub();
    private IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults> _mockEstablishmentResultsMapper
        = PageableSearchResultsToEstablishmentResultsMapperTestDouble.DefaultMock();
    private ISearchByKeywordService _mockSearchService;
    private ISearchFilterExpressionsBuilder _mockFilterExpressionBuilder = new FilterExpressionBuilderTestDouble().Create();

    private static CognitiveSearchServiceAdapter<DataTransferObjects.Establishment> CreateServiceAdapterWith(
        ISearchByKeywordService searchByKeywordService,
        IOptions<AzureSearchOptions> searchOptions,
        IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults> searchResponseMapper,
        IMapper<Dictionary<string, IList<Azure.Search.Documents.Models.FacetResult>>, EstablishmentFacets> facetsMapper,
        ISearchFilterExpressionsBuilder filterExpressionsBuilder
       ) =>
           new(searchByKeywordService, searchOptions, searchResponseMapper, facetsMapper, filterExpressionsBuilder);

    public CognitiveSearchServiceAdapterTests()
    {
        _mockSearchService = new SearchServiceMockBuilder().MockSearchService("SearchKeyword", _options.SearchIndex);
    }

    [Fact]
    public async Task Search_SendsPopulatedRequestToSearchService()
    {
        // arrange
        string? keywordPassedToSearchService = null;
        string? indexPassedToSearchService = null;
        SearchOptions? searchOptionsPassedToSearchService = null;

        var responseMock = new Mock<Response>();
        var searchServiceResponse = Response.FromValue(
                    SearchModelFactory.SearchResults(
                        new SearchResultFakeBuilder().WithSearchResults().Create(), 10, null, null, responseMock.Object), responseMock.Object);

        var mockService = new Mock<ISearchByKeywordService>();
        mockService.Setup(service => service.SearchAsync<DataTransferObjects.Establishment>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOptions>()))
            .Callback<string, string, SearchOptions>((x, y, z) =>
            {
                keywordPassedToSearchService = x;
                indexPassedToSearchService = y;
                searchOptionsPassedToSearchService = z;
            })
            .Returns(Task.FromResult(searchServiceResponse));

        var searchServiceAdapterRequest = SearchServiceAdapterRequestTestDouble.Create();

        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            CreateServiceAdapterWith(
                mockService.Object,
                IOptionsTestDouble.IOptionsMockFor(_options),
                _mockEstablishmentResultsMapper,
                _mockFacetsMapper,
                _mockFilterExpressionBuilder);

        // act
        var response = await cognitiveSearchServiceAdapter.SearchAsync(searchServiceAdapterRequest);

        // assert
        keywordPassedToSearchService.Should().Be(searchServiceAdapterRequest.SearchKeyword);
        indexPassedToSearchService.Should().Be(_options.SearchIndex);
        searchOptionsPassedToSearchService!.SearchFields.Should().BeEquivalentTo(searchServiceAdapterRequest.SearchFields);
        searchOptionsPassedToSearchService?.Facets.Should().BeEquivalentTo(searchServiceAdapterRequest.Facets);
    }

    [Fact]
    public void Search_WithFilters_CallsFilterBuilder()
    {
        // arrange
        var filterRequest = FilterRequestFake.Create();
        var serviceAdapterInputFilterRequest = new List<FilterRequest>() { filterRequest };
        var searchFilterExpressionsBuilderRequest = 
                new List<SearchFilterRequest>()
                {
                    new SearchFilterRequest(filterRequest.FilterName, filterRequest.FilterValues)
                };
        var mockSearchFilterExpressionsBuilder = new FilterExpressionBuilderTestDouble()
            .Create();

        var mockService = new SearchServiceMockBuilder().MockSearchService("SearchKeyword", _options.SearchIndex);
  
        var searchServiceAdapterRequest = SearchServiceAdapterRequestTestDouble.WithFilters(serviceAdapterInputFilterRequest);

        var adapter = new CognitiveSearchServiceAdapter<DataTransferObjects.Establishment>(
                _mockSearchService,
                IOptionsTestDouble.IOptionsMockFor<AzureSearchOptions>(_options),
                _mockEstablishmentResultsMapper,
                _mockFacetsMapper,
                mockSearchFilterExpressionsBuilder);

        // act
        var response = adapter.SearchAsync(searchServiceAdapterRequest);

        // assert
        Mock.Get(mockSearchFilterExpressionsBuilder).Verify();
    }

    [Fact]
    public void Search_WithNoSearchOptions_ThrowsApplicationException()
    {
        // act, assert
        try
        {
            var _ = new CognitiveSearchServiceAdapter<DataTransferObjects.Establishment>(
                _mockSearchService,
                IOptionsTestDouble.IOptionsMockFor<AzureSearchOptions>(null!),
                _mockEstablishmentResultsMapper,
                _mockFacetsMapper,
                _mockFilterExpressionBuilder);
            Assert.True(false);
        }
        catch (ArgumentNullException)
        {
            Assert.True(true);
        }
    }

    [Fact]
    public Task Search_MapperThrowsException_ExceptionPassesThrough()
    {
        // arrange
        var mockEstablishmentResultsMapper = 
            PageableSearchResultsToEstablishmentResultsMapperTestDouble.MockMapperThrowingArgumentException();

        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            CreateServiceAdapterWith(
                _mockSearchService,
                IOptionsTestDouble.IOptionsMockFor(_options),
                mockEstablishmentResultsMapper,
                _mockFacetsMapper,
                _mockFilterExpressionBuilder);

        // act, assert.
        return cognitiveSearchServiceAdapter
            .Invoking(adapter =>
                adapter.SearchAsync(new SearchServiceAdapterRequest(
                    searchKeyword: "SearchKeyword", [], [])))
            .Should()
            .ThrowAsync< ArgumentException>();
    }
}
