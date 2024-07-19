using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.Web.Mapping;
using Dfe.Data.SearchPrototype.Web.Models;
using Dfe.Data.SearchPrototype.Web.Tests.Unit.TestDoubles;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Xunit;

namespace Dfe.Data.SearchPrototype.Web.Tests.Unit;

public class SearchByKeywordResponseToViewModelMapperTests
{
    private readonly IMapper<SearchByKeywordResponse, SearchResultsViewModel> _serviceModelToViewModelMapper
        = new SearchByKeywordResponseToViewModelMapper();

    [Fact]
    public void Mapper_ReturnViewModel()
    {
        // arrange.
        var establishmentResults = SearchByKeywordResponseTestDouble.Create();

        // act.
        SearchResultsViewModel viewModelResults = _serviceModelToViewModelMapper.MapFrom(establishmentResults);

        // assert.
        for (int i=0; i< establishmentResults.EstablishmentResults?.Count; i++)
        {
            Assert.Equal(establishmentResults.EstablishmentResults.ToList()[i].Urn, viewModelResults.SearchItems![i].Urn);
            Assert.Equal(establishmentResults.EstablishmentResults.ToList()[i].Name, viewModelResults.SearchItems[i].Name);
        }
    }

    [Fact]
    public void Mapper_NoResults_ReturnViewModel_NullList()
    {
        // arrange.
        var establishmentResults = SearchByKeywordResponseTestDouble.CreateWithNoResults();

        // act.
        var viewModelResults = _serviceModelToViewModelMapper.MapFrom(establishmentResults);

        // assert.
        Assert.Null(viewModelResults.SearchItems);
    }
}
