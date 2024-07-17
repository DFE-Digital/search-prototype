using Dfe.Data.SearchPrototype.Search;
using Dfe.Data.SearchPrototype.Web.Mapping;
using Dfe.Data.SearchPrototype.Web.Models;
using Dfe.Data.SearchPrototype.Web.Tests.Unit.TestDoubles;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Xunit;

namespace Dfe.Data.SearchPrototype.Web.Tests.Unit;

public class ServiceModelToViewModelMapperTests
{
    private readonly IMapper<EstablishmentResults, SearchResultsViewModel> _serviceModelToViewModelMapper
        = new ServiceModelToViewModelMapper();

    [Fact]
    public void Mapper_ReturnViewModel()
    {
        // arrange.
        EstablishmentResults establishmentResults = EstablishmentResultsTestDouble.Create();

        // act.
        SearchResultsViewModel viewModelResults = _serviceModelToViewModelMapper.MapFrom(establishmentResults);

        // assert.
        for (int i=0; i< establishmentResults.Establishments.Count; i++)
        {
            Assert.Equal(establishmentResults.Establishments.ToList()[i].Urn, viewModelResults.searchItems[i].Urn);
            Assert.Equal(establishmentResults.Establishments.ToList()[i].Name, viewModelResults.searchItems[i].Name);
        }
    }

    [Fact]
    public void Mapper_NoResults_ReturnViewModel_EmptyList()
    {
        // arrange.
        EstablishmentResults establishmentResults = EstablishmentResultsTestDouble.CreateWithNoResults();

        // act.
        SearchResultsViewModel viewModelResults = _serviceModelToViewModelMapper.MapFrom(establishmentResults);

        // assert.
        Assert.Empty(viewModelResults.searchItems);
    }
}
