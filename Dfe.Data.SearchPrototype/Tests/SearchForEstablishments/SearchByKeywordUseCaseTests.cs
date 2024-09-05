using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.TestDoubles;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments;

public sealed class SearchByKeywordUseCaseTests
{
    private readonly SearchByKeywordUseCase _useCase;
    private ISearchServiceAdapter _searchServiceAdapter;
    private SearchResults _searchResults;

    public SearchByKeywordUseCaseTests()
    {
        // arrange
        _searchResults = SearchResultsTestDouble.Create();
        _searchServiceAdapter =
            SearchServiceAdapterTestDouble.MockFor(_searchResults);

        _useCase = new(_searchServiceAdapter);
    }

    [Fact]
    public async Task HandleRequest_ValidRequest_ReturnsResponse()
    {
        // arrange
        SearchByKeywordRequest request = new("searchkeyword", "target collection");

        // act
        SearchByKeywordResponse response = await _useCase.HandleRequest(request);

        // assert
        response.Status.Should().Be(SearchResponseStatus.Success);
        response.EstablishmentResults!.Establishments.Should().Contain(_searchResults.Establishments!.Establishments);
        response.EstablishmentFacetResults!.Facets.Should().Contain(_searchResults.Facets!.Facets);
    }

     [Fact]
    public async Task HandleRequest_NullSearchByKeywordRequest_ReturnsErrorStatus()
    {
        // act
        var response = await _useCase.HandleRequest(request: null!);

        // assert
        response.Status.Should()
                .Be(SearchResponseStatus.InvalidRequest);
    }

    [Fact]
    public async Task HandleRequest_ServiceAdapterThrowsException_ReturnsErrorStatus()
    {
        // arrange
        SearchByKeywordRequest request = new("searchkeyword", "target collection");
        Mock.Get(_searchServiceAdapter)
            .Setup(adapter => adapter.SearchAsync(It.IsAny<SearchContext>()))
            .ThrowsAsync(new ApplicationException());

        // act
        var response = await _useCase.HandleRequest(request);

        // assert
        response.Status.Should()
                .Be(SearchResponseStatus.SearchServiceError);
    }

    [Fact]
    public async Task HandleRequest_NoResults_ReturnsSuccess()
    {
        // arrange
        SearchByKeywordRequest request = new("searchkeyword", "target collection");
        Mock.Get(_searchServiceAdapter)
            .Setup(adapter => adapter.SearchAsync(It.IsAny<SearchContext>()))
            .ReturnsAsync(SearchResultsTestDouble.CreateWithNoResults);

        // act
        var response = await _useCase.HandleRequest(request);

        // assert
        response.Status.Should()
                .Be(SearchResponseStatus.Success);
    }
}
