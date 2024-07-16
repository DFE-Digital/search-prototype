using Dfe.Data.SearchPrototype.Search;
using Dfe.Data.SearchPrototype.Web.Mapping;
using Dfe.Data.SearchPrototype.Web.Models;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dfe.Data.SearchPrototype.Web.Tests.Unit;

public class ServiceModelToViewModelMapperTests
{
    [Fact]
    public void Mapper_ReturnViewModel()
    {
        // arrange.
        EstablishmentResults establishmentResults = new EstablishmentResults();
        Establishment establishment = new Establishment("urn", "name");
        establishmentResults.AddEstablishment(establishment);

        IMapper<EstablishmentResults, SearchResultsViewModel> serviceModelToViewModelMapper = new ServiceModelToViewModelMapper();

        // act.

        SearchResultsViewModel viewModelResults = serviceModelToViewModelMapper.MapFrom(establishmentResults);



        // assert.

        Assert.Equal(viewModelResults.searchItems.First().Urn, establishmentResults.Establishments.First().Urn);
        Assert.Equal(viewModelResults.searchItems.First().Name, establishmentResults.Establishments.First().Name);
    }
}
