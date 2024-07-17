using DfE.Data.ComponentLibrary.CleanArchitecture.CleanArchitecture.Application.UseCase;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Xunit;
using Moq;
using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using FluentAssertions;
using Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.TestDoubles;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments;

public class SearchByKeywordUseCaseTests
{
    [Fact]
    public async Task UseCase_ValidRequest_ReturnsValidResponse()
    {
        // arrange.
        Mock<ISearchServiceAdapter> searchServiceAdapter = new Mock<ISearchServiceAdapter>();
        searchServiceAdapter.Setup(x => x.SearchAsync(It.IsAny<SearchContext>())).ReturnsAsync(EstablishmentResultsTestDouble.Create());
        IMapper<EstablishmentResults, SearchByKeywordResponse> mapper = new ResultsToResponseMapper();
        SearchByKeywordUseCase useCase = new(searchServiceAdapter.Object, mapper);
        
        // act.
        SearchByKeywordRequest request = new() { Context = new(searchKeyword: "anything", targetCollection : "collection") };
        SearchByKeywordResponse response = await useCase.HandleRequest(request);

        // assert
        response.Should().NotBeNull();
    }
}
