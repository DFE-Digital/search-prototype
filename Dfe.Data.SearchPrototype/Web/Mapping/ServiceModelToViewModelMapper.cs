using Dfe.Data.SearchPrototype.Search;
using Dfe.Data.SearchPrototype.Web.Models;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.Web.Mapping;

public class ServiceModelToViewModelMapper : IMapper<EstablishmentResults, SearchResultsViewModel>
{
    public SearchResultsViewModel MapFrom(EstablishmentResults input)
    {
        SearchResultsViewModel viewModel = new();

        foreach (var establishment in input.Establishments)
        {
            viewModel.searchItems.Add(new SearchItemViewModel
            {

                Urn = establishment.Urn,

                Name = establishment.Name

            });
        }
        return viewModel;
    }
}
