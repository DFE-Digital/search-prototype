using Dfe.Data.SearchPrototype.Search;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.Web.Models;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.Web.Mapping;

public class SearchByKeywordResponseToViewModelMapper : IMapper<SearchByKeywordResponse, SearchResultsViewModel>
{
    public SearchResultsViewModel MapFrom(SearchByKeywordResponse input)
    {
        SearchResultsViewModel viewModel = new();

        if (input.EstablishmentResults != null)
        {
            viewModel.SearchItems = new();
            foreach (var establishment in input.EstablishmentResults)
            {
                viewModel.SearchItems.Add(new SearchItemViewModel
                {
                    Urn = establishment.Urn,
                    Name = establishment.Name
                });
            }
        }
        return viewModel;
    }
}
