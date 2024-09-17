using Azure;
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
    private ISearchFilterExpressionsBuilder _mockFilterExpressionBuilder = new FilterExpressionBuilderTestDouble().Create();

    private static CognitiveSearchServiceAdapter<DataTransferObjects.Establishment> CreateServiceAdapterWith(
        ISearchByKeywordService searchByKeywordService,
        IOptions<AzureSearchOptions> searchOptions,
        IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults> searchResponseMapper,
        IMapper<Dictionary<string, IList<Azure.Search.Documents.Models.FacetResult>>, EstablishmentFacets> facetsMapper,
        ISearchFilterExpressionsBuilder filterExpressionsBuilder
       ) =>
           new(searchByKeywordService, searchOptions, searchResponseMapper, facetsMapper, filterExpressionsBuilder);

    [Fact]
    public void Search_WithFilters_CallsFilterBuilder()
    {
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

        var adapter = new CognitiveSearchServiceAdapter<DataTransferObjects.Establishment>(mockService,
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
        var mockService = new SearchServiceMockBuilder().MockSearchService("SearchKeyword", "");

        // act
        try
        {
            var _ = new CognitiveSearchServiceAdapter<DataTransferObjects.Establishment>(mockService,
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
        var mockService = new SearchServiceMockBuilder().MockSearchService("SearchKeyword", _options.SearchIndex);
        var mockEstablishmentResultsMapper = 
            PageableSearchResultsToEstablishmentResultsMapperTestDouble.MockMapperThrowingArgumentException();

        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            CreateServiceAdapterWith(
                mockService,
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
