﻿using Dfe.Data.SearchPrototype.Search;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.TestDoubles;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments;

public sealed class SearchByKeywordUseCaseTests
{
    private readonly SearchByKeywordUseCase _useCase;

    public SearchByKeywordUseCaseTests()
    {
        // arrange
        ISearchServiceAdapter searchServiceAdapter =
            SearchServiceAdapterTestDouble.MockFor(
                EstablishmentResultsTestDouble.Create());

        IMapper<EstablishmentResults, SearchByKeywordResponse> mapper = new ResultsToResponseMapper();
        _useCase = new(searchServiceAdapter, mapper);
    }

    [Fact]
    public async Task UseCase_ValidRequest_ReturnsResponse()
    {
        // arrange
        SearchByKeywordRequest request = new()
        {
            Context = new(searchKeyword: "anything", targetCollection: "collection")
        };

        // act
        SearchByKeywordResponse response = await _useCase.HandleRequest(request);

        // assert
        response.Should().NotBeNull();
    }

    [Fact]
    public Task UseCase_NullSearchByKeywordRequest_ThrowsArgumentNullException()
    {
        // act, assert
        return _useCase.Invoking(
            async usecase => await usecase
                .HandleRequest(request: null!))
                .Should()
                .ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'SearchByKeywordRequest')");
    }

    [Fact]
    public Task UseCase_NullSearchContext_ThrowsArgumentNullException()
    {
        // arrange
        SearchByKeywordRequest request = new();

        // act, assert
        return _useCase.Invoking(
            async usecase => await usecase
                .HandleRequest(request))
                .Should()
                .ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'SearchContext')");
    }
}