﻿using Dfe.Data.SearchPrototype.SearchForEstablishments;
using DfE.Data.ComponentLibrary.CleanArchitecture.CleanArchitecture.Application.UseCase;
using Moq;

namespace Dfe.Data.SearchPrototype.Web.Tests.Unit.TestDoubles;

public static class SearchByKeywordUseCaseTestDouble
{
    public static Mock<IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>> MockFor(SearchByKeywordResponse response)
    {
        Mock<IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>> useCaseMock = DefaultMock();

        useCaseMock.Setup(useCase =>
            useCase.HandleRequest(It.IsAny<SearchByKeywordRequest>())).Returns(Task.FromResult(response));

        return useCaseMock;
    }

    public static Mock<IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>> DefaultMock()
    {
        return new Mock<IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>> ();
    }
}
