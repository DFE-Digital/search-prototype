﻿using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Filtering;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.Shared;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests;

public sealed class CognitiveSearchServiceAdapterAndMapperTests
{
    private readonly IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults> _searchResponseMapper;
    private readonly IMapper<Dictionary<string, IList<Azure.Search.Documents.Models.FacetResult>>, EstablishmentFacets> _facetsMapper;
    private readonly ISearchFilterExpressionsBuilder _mockSearchFilterExpressionsBuilder = 
        new FilterExpressionBuilderTestDouble()
            .WithResponse("some_filter_name le some_value")
            .Create();

    public CognitiveSearchServiceAdapterAndMapperTests()
    {
        _searchResponseMapper = new PageableSearchResultsToEstablishmentResultsMapper(
            new AzureSearchResultToEstablishmentMapper(
                new AzureSearchResultToAddressMapper()));
        _facetsMapper = new AzureFacetResultToEstablishmentFacetsMapper();
    }

    [Fact]
    public async Task Search_WithValidSearchContext_ReturnsResults()
    {
        // arrange
        var establishmentSearchResults = new SearchResultFakeBuilder()
                .WithSearchResults()
                .Create();
        var facetResults = new FacetsResultsFakeBuilder()
                .WithAutoGeneratedFacets()
                .Create();
        var options = AzureSearchOptionsTestDouble.Stub();
        var mockService = new SearchServiceMockBuilder()
            .WithSearchKeywordAndCollection("SearchKeyword", options.SearchIndex)
            .WithSearchResults(establishmentSearchResults)
            .WithFacets(facetResults)
            .Create();

        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            new CognitiveSearchServiceAdapter<DataTransferObjects.Establishment>(
                mockService,
                IOptionsTestDouble.IOptionsMockFor(options),
                _searchResponseMapper,
                _facetsMapper,
                _mockSearchFilterExpressionsBuilder);

        // act
        SearchResults? response =
            await cognitiveSearchServiceAdapter.SearchAsync(
                new SearchServiceAdapterRequest(
                    searchKeyword: "SearchKeyword",
                    searchFields: ["FIELD1", "FIELD2", "FIELD2"],
                    facets: ["FACET1", "FACET2", "FACET3"]));

        // assert
        response.Should().NotBeNull();
        response.Establishments.Should().NotBeNull();
        response.Establishments!.Establishments.Count().Should().Be(establishmentSearchResults.Count);
        response.Facets.Should().NotBeNull();
        response.Facets!.Facets.Count().Should().Be(facetResults.Count());
    }

    [Fact]
    public async Task Search_WithNoFacetsReturned_ReturnsNullFacets()
    {
        // arrange
        var options = AzureSearchOptionsTestDouble.Stub();
        var mockService = new SearchServiceMockBuilder()
            .WithSearchKeywordAndCollection("SearchKeyword", options.SearchIndex)
            .WithSearchResults(
                new SearchResultFakeBuilder()
                .WithSearchResults()
                .Create())
            .Create();

        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            new CognitiveSearchServiceAdapter<DataTransferObjects.Establishment>(
                mockService,
                IOptionsTestDouble.IOptionsMockFor(options),
                _searchResponseMapper,
                _facetsMapper,
                _mockSearchFilterExpressionsBuilder);

        // act
        SearchResults? response =
            await cognitiveSearchServiceAdapter.SearchAsync(
                new SearchServiceAdapterRequest(
                    searchKeyword: "SearchKeyword",
                    searchFields: ["FIELD1", "FIELD2", "FIELD2"],
                    facets: ["FACET1", "FACET2", "FACET3"]));

        // assert
        response.Should().NotBeNull();
        response.Facets.Should().BeNull();
    }

    [Fact]
    public async Task Search_WithNoResultsReturned_ReturnsEmptyResults()
    {
        // arrange
        var options = AzureSearchOptionsTestDouble.Stub();
        var mockService = new SearchServiceMockBuilder()
            .WithSearchKeywordAndCollection("SearchKeyword", options.SearchIndex)
            .WithSearchResults(
                new SearchResultFakeBuilder()
                .WithEmptySearchResult()
                .Create())
            .Create();

        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            new CognitiveSearchServiceAdapter<DataTransferObjects.Establishment>(
                mockService,
                IOptionsTestDouble.IOptionsMockFor(options),
                _searchResponseMapper,
                _facetsMapper,
                _mockSearchFilterExpressionsBuilder);

        // act.
        var response =
            await cognitiveSearchServiceAdapter.SearchAsync(
                new SearchServiceAdapterRequest(
                    searchKeyword: "SearchKeyword",
                    searchFields: ["FIELD1", "FIELD2", "FIELD2"],
                    facets: ["FACET1", "FACET2", "FACET3"]));

        // assert
        response.Should().NotBeNull();
        response.Establishments.Should().NotBeNull();
        response.Establishments!.Establishments.Should().BeEmpty();
    }
}
