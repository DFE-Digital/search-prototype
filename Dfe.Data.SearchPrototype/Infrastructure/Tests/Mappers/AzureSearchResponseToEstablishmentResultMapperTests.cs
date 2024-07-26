using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestHelpers;
using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mappers;

public sealed class AzureSearchResponseToEstablishmentResultMapperTests
{
    IMapper<Establishment, Search.Establishment> _azureSearchResultToEstablishmentMapper;
    IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> _establishmentMapper;

    public AzureSearchResponseToEstablishmentResultMapperTests()
    {
        _azureSearchResultToEstablishmentMapper =
            new AzureSearchResultToEstablishmentMapper();
        _establishmentMapper = 
            new AzureSearchResponseToEstablishmentResultMapper(_azureSearchResultToEstablishmentMapper);
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
        EstablishmentResults? mappedResult = _establishmentMapper.MapFrom(searchResponseFake);

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
        EstablishmentResults? result = _establishmentMapper.MapFrom(searchResponseFake);

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
        _establishmentMapper
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
        _establishmentMapper
            .Invoking(mapper =>
                mapper.MapFrom(searchResponseFake))
                    .Should()
                        .Throw<InvalidOperationException>()
                        .WithMessage("Search result document object cannot be null.");
    }
}