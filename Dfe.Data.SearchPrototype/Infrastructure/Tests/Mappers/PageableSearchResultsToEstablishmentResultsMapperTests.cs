using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestHelpers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mappers;

public sealed class PageableSearchResultsToEstablishmentResultsMapperTests
{
    IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults> _searchResultsMapper;

    public PageableSearchResultsToEstablishmentResultsMapperTests()
    {
        _searchResultsMapper =
            new PageableSearchResultsToEstablishmentResultsMapper(
                new AzureSearchResultToEstablishmentMapper(
                    new AzureSearchResultToAddressMapper()));
    }

    [Fact]
    public void MapFrom_WithValidSearchResults_ReturnsConfiguredEstablishments()
    {
        // arrange
        List<SearchResult<DataTransferObjects.Establishment>> searchResultDocuments =
            SearchResultFake.SearchResults();

        var pageableSearchResults = PageableTestDouble.FromResults(searchResultDocuments);

        // act
        EstablishmentResults? mappedResult = _searchResultsMapper.MapFrom(pageableSearchResults);

        // assert
        mappedResult.Should().NotBeNull();
        mappedResult.Establishments.Should().HaveCount(searchResultDocuments.Count);

        foreach (var searchResult in searchResultDocuments)
        {
            searchResult.ShouldHaveMatchingMappedEstablishment(mappedResult);
        }
    }

    [Fact]
    public void MapFrom_WithEmptySearchResults_ReturnsEmptyList()
    {
        // act
        EstablishmentResults? result =
            _searchResultsMapper.MapFrom(
                PageableTestDouble.FromResults(SearchResultFake.EmptySearchResult()));

        // assert
        result.Should().NotBeNull();
        result.Establishments.Should().HaveCount(0);
    }

    [Fact]
    public void MapFrom_WithNullSearchResults_ThrowsArgumentNullException()
    {
        // act.
        _searchResultsMapper
            .Invoking(mapper =>
                mapper.MapFrom(null!))
                    .Should()
                        .Throw<ArgumentNullException>()
                        .WithMessage("Value cannot be null. (Parameter 'input')");
    }

    [Fact]
    public void MapFrom_WithANullSearchResult_ThrowsInvalidOperationException()
    {
        // arrange
        var searchResultDocuments =
            SearchResultFake.SearchResults();
        searchResultDocuments.Add(SearchResultFake.SearchResultWithDocument(null));

        // act.
        _searchResultsMapper
            .Invoking(mapper =>
                mapper.MapFrom(PageableTestDouble.FromResults(searchResultDocuments)))
                    .Should()
                        .Throw<InvalidOperationException>()
                        .WithMessage("Search result document object cannot be null.");
    }
}