using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.Shared;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests;

public sealed class CognitiveSearchServiceAdapterTests
{
    private static CognitiveSearchServiceAdapter<DataTransferObjects.Establishment> CreateServiceAdapterWith(
        ISearchByKeywordService searchByKeywordService,
        IOptions<AzureSearchOptions> searchOptions,
        IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults> searchResponseMapper,
        IMapper<Dictionary<string, IList<Azure.Search.Documents.Models.FacetResult>>, EstablishmentFacets> facetsMapper
       ) =>
           new(searchByKeywordService, searchOptions, searchResponseMapper, facetsMapper);

    [Fact]
    public void Search_WithNoSearchOptions_ThrowsApplicationException()
    {
        var mockService = new SearchServiceMockBuilder().MockSearchService("SearchKeyword", "");
        var mockEstablishmentResultsMapper = PageableSearchResultsToEstablishmentResultsMapperTestDouble.DefaultMock();
        var mockFacetsMapper = AzureFacetResultToEstablishmentFacetsMapperTestDouble.DefaultMock();

        // act
        try
        {
            var _ = new CognitiveSearchServiceAdapter<DataTransferObjects.Establishment>(mockService,
                IOptionsTestDouble.IOptionsMockFor<AzureSearchOptions>(null!),
                mockEstablishmentResultsMapper,
                mockFacetsMapper);
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
        var options = AzureSearchOptionsTestDouble.Stub();
        var mockService = new SearchServiceMockBuilder().MockSearchService("SearchKeyword", options.SearchIndex);
        var mockEstablishmentResultsMapper = PageableSearchResultsToEstablishmentResultsMapperTestDouble.MockMapperThrowingArgumentException();
        var mockFacetsMapper = AzureFacetResultToEstablishmentFacetsMapperTestDouble.DefaultMock();

        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            CreateServiceAdapterWith(
                mockService,
                IOptionsTestDouble.IOptionsMockFor(options),
                mockEstablishmentResultsMapper,
                mockFacetsMapper);

        // act, assert.
        return cognitiveSearchServiceAdapter
            .Invoking(adapter =>
                adapter.SearchAsync(new SearchServiceAdapterRequest(
                    searchKeyword: "SearchKeyword", [], [])))
            .Should()
            .ThrowAsync< ArgumentException>();
    }
}
