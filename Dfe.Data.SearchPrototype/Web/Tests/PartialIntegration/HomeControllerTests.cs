using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.Web.Controllers;
using Microsoft.Extensions.Logging;
using Dfe.Data.SearchPrototype.Search;
using Moq;
using Dfe.Data.SearchPrototype.Web.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentAssertions;
using Dfe.Data.SearchPrototype.Web.Tests.PartialIntegration.TestDoubles;
using Dfe.Data.SearchPrototype.Web.Models;
using DfE.Data.ComponentLibrary.CleanArchitecture.CleanArchitecture.Application.UseCase;

namespace Dfe.Data.SearchPrototype.Web.Tests.PartialIntegration;

public class HomeControllerTests
{
    private readonly Mock<ILogger<HomeController>> _logger = new();
    private readonly Mock<ISearchServiceAdapter> _searchServiceAdapterMock = new();

    [Fact]
    public async Task Index_WithSearchTerm_ReturnsModel()
    {
        // arrange
        var stubSearchResults = EstablishmentResultsTestDouble.Create();
        _searchServiceAdapterMock.Setup(adapter => adapter.SearchAsync(It.IsAny<SearchContext>()))
            .ReturnsAsync(stubSearchResults);
        var useCase = new SearchByKeywordUseCase(_searchServiceAdapterMock.Object, new ResultsToResponseMapper());
        var controller = new HomeController(_logger.Object, useCase, new SearchByKeywordResponseToViewModelMapper());

        // act
        IActionResult result = await controller.Index("searchTerm");

        // assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var viewModel = Assert.IsType<SearchResultsViewModel>(viewResult.Model);
        viewModel.SearchItems.Should().NotBeEmpty();
        viewModel.HasResults.Should().BeTrue();
        viewModel.SearchResultsCount.Should().Be(stubSearchResults.Establishments.Count);
    }

    [Fact]
    public async Task Index_WithNoResults_ReturnsNoModel()
    {
        // arrange
        _searchServiceAdapterMock.Setup(adapter => adapter.SearchAsync(It.IsAny<SearchContext>()))
            .ReturnsAsync(EstablishmentResultsTestDouble.CreateWithNoResults());
        var useCase = new SearchByKeywordUseCase(_searchServiceAdapterMock.Object, new ResultsToResponseMapper());
        var controller = new HomeController(_logger.Object, useCase, new SearchByKeywordResponseToViewModelMapper());

        // act
        IActionResult result = await controller.Index("searchTerm");

        // assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var viewModel = Assert.IsType<SearchResultsViewModel>(viewResult.Model);
        viewModel.SearchItems.Should().BeEmpty();
        viewModel.HasResults.Should().BeFalse();
        viewModel.SearchResultsCount.Should().Be(0);
    }
}
