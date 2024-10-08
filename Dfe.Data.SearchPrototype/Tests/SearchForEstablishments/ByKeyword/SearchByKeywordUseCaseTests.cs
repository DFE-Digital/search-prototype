using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.Shared;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.ByKeyword.TestDoubles;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.ByKeyword;

public sealed class SearchByKeywordUseCaseTests
{
    private readonly SearchByKeywordUseCase _useCase;
    private readonly ISearchServiceAdapter _searchServiceAdapter;
    private readonly SearchResults _searchResults;
    private readonly SearchByKeywordCriteria _searchByKeywordCriteriaStub = SearchByKeywordCriteriaTestDouble.Create();

    public SearchByKeywordUseCaseTests()
    {
        // arrange
        _searchResults = SearchResultsTestDouble.Create();
        _searchServiceAdapter =
            SearchServiceAdapterTestDouble.MockFor(_searchResults);

        _useCase = new(_searchServiceAdapter, _searchByKeywordCriteriaStub);
    }

    [Fact]
    public async Task HandleRequest_ValidRequest_CallsAdapterWithMappedRequestParams()
    {
        // arrange
        SearchServiceAdapterRequest? adapterRequest = null;
        Mock.Get(_searchServiceAdapter)
            .Setup(adapter => adapter.SearchAsync(It.IsAny<SearchServiceAdapterRequest>()))
            .Callback<SearchServiceAdapterRequest>((input) =>
            {
                adapterRequest = input;
            });

        SearchByKeywordRequest request = new("searchkeyword", new List<FilterRequest>() { FilterRequestFake.Create() });

        // act
        SearchByKeywordResponse response = await _useCase.HandleRequest(request);

        // assert
        adapterRequest!.SearchKeyword.Should().Be(request.SearchKeyword);
        adapterRequest!.SearchFields.Should().BeEquivalentTo(_searchByKeywordCriteriaStub.SearchFields);
        adapterRequest!.Facets.Should().BeEquivalentTo(_searchByKeywordCriteriaStub.Facets);
        adapterRequest!.SearchFilterRequests.Should().BeEquivalentTo(request.FilterRequests);
    }

    [Fact]
    public async Task HandleRequest_ValidRequest_ReturnsResponse()
    {
        // arrange
        SearchByKeywordRequest request = new("searchkeyword");

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
        SearchByKeywordRequest request = new("searchkeyword");
        Mock.Get(_searchServiceAdapter)
            .Setup(adapter => adapter.SearchAsync(It.IsAny<SearchServiceAdapterRequest>()))
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
        SearchByKeywordRequest request = new("searchkeyword");
        Mock.Get(_searchServiceAdapter)
            .Setup(adapter => adapter.SearchAsync(It.IsAny<SearchServiceAdapterRequest>()))
            .ReturnsAsync(SearchResultsTestDouble.CreateWithNoResults);

        // act
        var response = await _useCase.HandleRequest(request);

        // assert
        response.Status.Should()
                .Be(SearchResponseStatus.Success);
    }
}
