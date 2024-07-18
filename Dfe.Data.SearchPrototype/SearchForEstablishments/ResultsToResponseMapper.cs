using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// 
/// </summary>
public class ResultsToResponseMapper : IMapper<EstablishmentResults, SearchByKeywordResponse>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public SearchByKeywordResponse MapFrom(EstablishmentResults input)
    {
       SearchByKeywordResponse response = new(input.Establishments);

        return response;
    }
}
