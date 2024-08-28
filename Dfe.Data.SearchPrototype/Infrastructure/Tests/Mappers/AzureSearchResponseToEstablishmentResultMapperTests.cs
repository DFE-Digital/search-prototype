﻿using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestHelpers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mappers;

public sealed class AzureSearchResponseToEstablishmentResultMapperTests
{
    IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> _searchResponseMapper;

    public AzureSearchResponseToEstablishmentResultMapperTests()
    {
        _searchResponseMapper =
            new AzureSearchResponseToEstablishmentResultMapper(
                new AzureSearchResultToEstablishmentMapper(
                    new AzureSearchResultToAddressMapper()));
    }

    [Fact]
    public void MapFrom_WithValidSearchResults_ReturnsConfiguredEstablishments()
    {
        // arrange
        List<SearchResult<Establishment>> searchResultDocuments =
            new SearchResultFakeBuilder().WithSearchResults().Create();
        Response<SearchResults<Establishment>> searchResponseFake =
            new ResponseFake().WithSearchResults(searchResultDocuments).Create();

        // act
        EstablishmentResults? mappedResult = _searchResponseMapper.MapFrom(searchResponseFake);

        // assert
        mappedResult.Should().NotBeNull();
        mappedResult.Establishments.Should().HaveCount(searchResultDocuments.Count());
        foreach (var searchResult in searchResultDocuments)
        {
            searchResult.ShouldHaveMatchingMappedEstablishment(mappedResult);
        }
    }

    [Fact]
    public void MapFrom_WithNoFacets_ReturnsNullFacetDictionary()
    {
        // arrange
        Response<SearchResults<Establishment>> searchResponseFake =
            new ResponseFake()
                .WithSearchResults(
                    new SearchResultFakeBuilder().WithSearchResults().Create())
                .Create();

        // act
        EstablishmentResults? mappedResult = _searchResponseMapper.MapFrom(searchResponseFake);

        // assert
        mappedResult.Should().NotBeNull();
        mappedResult.Facets.Should().BeNull();
    }

    [Fact]
    public void MapFrom_WithFacetResults_ReturnsFacetDictionary()
    {
        // arrange
        var searchResultsDocuments = new SearchResultFakeBuilder()
            .WithSearchResults()
            .Create();

        Response<SearchResults<Establishment>> searchResponseFake =
            new ResponseFake().WithSearchResults(searchResultsDocuments).Create();
    }

    [Fact]
    public void MapFrom_WithEmptySearchResults_ReturnsEmptyList()
    {
        // arrange
        var searchResultsDocuments = new SearchResultFakeBuilder()
            .WithEmptySearchResult()
            .Create();

        Response<SearchResults<Establishment>> searchResponseFake =
            new ResponseFake().WithSearchResults(searchResultsDocuments).Create();

        // act
        EstablishmentResults? result = _searchResponseMapper.MapFrom(searchResponseFake);

        // assert
        result.Should().NotBeNull();
        result.Establishments.Should().HaveCount(0);
    }

    [Fact]
    public void MapFrom_WithNullSearchResults_ThrowsArgumentNullException()
    {
        // arrange
        Response<SearchResults<Establishment>>? searchResponseFake = null;

        // act.
        _searchResponseMapper
            .Invoking(mapper =>
                mapper.MapFrom(searchResponseFake!))
                    .Should()
                        .Throw<ArgumentNullException>()
                        .WithMessage("Value cannot be null. (Parameter 'input')");
    }

    [Fact]
    public void MapFrom_WithANullSearchResult_ThrowsInvalidOperationException()
    {
        // arrange
        var searchResultDocuments =
            new SearchResultFakeBuilder()
                .WithSearchResults()
                .IncludeNullDocument()
                .Create();

        Response<SearchResults<Establishment>> searchResponseFake =
                    new ResponseFake().WithSearchResults(searchResultDocuments).Create();

        // act.
        _searchResponseMapper
            .Invoking(mapper =>
                mapper.MapFrom(searchResponseFake))
                    .Should()
                        .Throw<InvalidOperationException>()
                        .WithMessage("Search result document object cannot be null.");
    }
}