using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// Facilitates mapping from the received T:Dfe.Data.SearchPrototype.Search.EstablishmentResults
/// into the required T:Dfe.Data.SearchPrototype.SearchForEstablishments.SearchByKeywordResponse object.
/// </summary>
public class ResultsToResponseMapper : IMapper<SearchResults, SearchByKeywordResponse>
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
    public SearchByKeywordResponse MapFrom(SearchResults input)
    {
        SearchResponseStatus status = (input == null)
            ? SearchResponseStatus.SearchServiceError
            :  SearchResponseStatus.Success;

        if (status == SearchResponseStatus.Success)
        {
            return new(status) { EstablishmentResults = input!.Establishments, EstablishmentFacetResults = input.Facets };
        }
        else
        {
            return new(status);
        }
    }
}
