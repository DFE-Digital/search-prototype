using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Search;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// Facilitates mapping from the received T:Dfe.Data.SearchPrototype.Search.EstablishmentResults
/// into the required T:Dfe.Data.SearchPrototype.SearchForEstablishments.SearchByKeywordResponse object.
/// </summary>
public class ResultsToResponseMapper : IMapper<EstablishmentResults, SearchByKeywordResponse>
{
    /// <summary>
    /// The mapping input is the T:Dfe.Data.SearchPrototype.Search.EstablishmentResults which, if successful,
    /// a new T:Dfe.Data.SearchPrototype.SearchForEstablishments.SearchByKeywordResponse
    /// instance is created.
    /// </summary>
    /// <param name="input">
    /// A configured T:Dfe.Data.SearchPrototype.Search.EstablishmentResults instance.
    /// </param>
    /// <returns>
    /// A configured T:Dfe.Data.SearchPrototype.SearchForEstablishments.SearchByKeywordResponse instance.
    /// </returns>
    public SearchByKeywordResponse MapFrom(EstablishmentResults input)
    {
        SearchByKeywordResponse response = new(input.Establishments);

        return response;
    }
}
