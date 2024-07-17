using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

public class ResultsToResponseMapper : IMapper<EstablishmentResults, SearchByKeywordResponse>
{
    public SearchByKeywordResponse MapFrom(EstablishmentResults input)
    {
       SearchByKeywordResponse response = new(input.Establishments);

        return response;
    }
}
