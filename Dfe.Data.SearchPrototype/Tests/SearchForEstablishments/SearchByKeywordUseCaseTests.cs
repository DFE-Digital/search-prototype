using Dfe.Data.SearchPrototype.Common.Mappers;
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
    private IMapper<EstablishmentResults, SearchByKeywordResponse> _mapper;

    public SearchByKeywordUseCaseTests()
    {
        // arrange
        _searchServiceAdapter =
            SearchServiceAdapterTestDouble.MockFor(
                EstablishmentResultsTestDouble.Create());

        _mapper = new ResultsToResponseMapper();
        _useCase = new(_searchServiceAdapter, _mapper);
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
    public async Task HandleRequest_ServiceAdapterIncorrectSetup_ReturnsErrorStatus()
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
            .ReturnsAsync(EstablishmentResultsTestDouble.CreateWithNoResults);

        // act
        var response = await _useCase.HandleRequest(request);

        // assert
        response.Status.Should()
                .Be(SearchResponseStatus.Success);
    }
}
