using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestHelpers;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mappers;

public sealed class AzureSearchResponseToEstablishmentResultMapperTests
{
    IMapper<Establishment, SearchForEstablishments.Establishment> _searchResultToEstablishmentMapper;
    IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> _searchResponseMapper;
    IMapper<Establishment, SearchForEstablishments.Address> _searchResultToAddressMapper;

    public AzureSearchResponseToEstablishmentResultMapperTests()
    {
        _searchResultToAddressMapper = new AzureSearchResultToAddressMapper();
        _searchResultToEstablishmentMapper =
            new AzureSearchResultToEstablishmentMapper(_searchResultToAddressMapper);
        _searchResponseMapper = 
            new AzureSearchResponseToEstablishmentResultMapper(_searchResultToEstablishmentMapper);
    }

    [Fact]
    public void MapFrom_WithValidSearchResults_ReturnsConfiguredEstablishments()
    {
        // arrange
        List<SearchResult<Establishment>> searchResultDocuments =
            SearchResultFake.SearchResults();
        Response<SearchResults<Establishment>> searchResponseFake =
            ResponseFake.WithSearchResults(searchResultDocuments);

        // act
        EstablishmentResults? mappedResult = _searchResponseMapper.MapFrom(searchResponseFake);

        // assert
        mappedResult.Should().NotBeNull();
        mappedResult.Establishments.Should().HaveCount(searchResultDocuments.Count());
        foreach(var searchResult in searchResultDocuments)
        {
            searchResult.ShouldHaveMatchingMappedEstablishment(mappedResult);
        }
    }

    [Fact]
    public void MapFrom_WithEmptySearchResults_ReturnsEmptyList()
    {
        // arrange
        Response<SearchResults<Establishment>> searchResponseFake =
            ResponseFake.WithSearchResults(SearchResultFake.EmptySearchResult());

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
            SearchResultFake.SearchResults();
        searchResultDocuments.Add(SearchResultFake.SearchResultWithDocument(null));
        Response<SearchResults<Establishment>> searchResponseFake =
                    ResponseFake.WithSearchResults(searchResultDocuments);

        // act.
        _searchResponseMapper
            .Invoking(mapper =>
                mapper.MapFrom(searchResponseFake))
                    .Should()
                        .Throw<InvalidOperationException>()
                        .WithMessage("Search result document object cannot be null.");
    }
}