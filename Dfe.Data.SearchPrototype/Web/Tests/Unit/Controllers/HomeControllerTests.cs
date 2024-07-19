﻿using Dfe.Data.SearchPrototype.Search;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.Web.Controllers;
using Dfe.Data.SearchPrototype.Web.Models;
using Dfe.Data.SearchPrototype.Web.Tests.Unit.TestDoubles;
using DfE.Data.ComponentLibrary.CleanArchitecture.CleanArchitecture.Application.UseCase;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Dfe.Data.SearchPrototype.Web.Tests.Unit.Controllers;


public class HomeControllerTests
{
    [Fact]
    public async Task Index_CallUseCase()
    {
        Mock<ILogger<HomeController>> mockLogger = LoggerTestDouble.MockLogger();
        Mock<IMapper<SearchByKeywordResponse, SearchResultsViewModel>> mockMapper = 
            SearchResultsToViewModelMapperTestDouble.MockFor(new SearchResultsViewModel());
        SearchByKeywordResponse response = new(new List<Establishment>().AsReadOnly());
        Mock<IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>> mockUseCase = 
            SearchByKeywordUseCaseTestDouble.MockFor(response);

        HomeController controller = new(mockLogger.Object, mockUseCase.Object, mockMapper.Object);

        IActionResult result = await controller.Index("KDM");

        mockUseCase.Verify(useCase => useCase.HandleRequest(It.IsAny<SearchByKeywordRequest>()), Times.Once());
        result.Should().NotBeNull();
    }
}
