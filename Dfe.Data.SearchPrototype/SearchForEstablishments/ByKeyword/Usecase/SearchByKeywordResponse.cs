using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;

/// <summary>
/// This is the object that carries the response (output) back from the
/// <see cref="SearchByKeywordUseCase" /> instance.
/// </summary>
public sealed class SearchByKeywordResponse
{
    /// <summary>
    /// The result object that encapsulates the <see cref="Establishment"/>
    /// search results returned by the Establishment search.
    /// </summary>
    public EstablishmentResults? EstablishmentResults { get; init; }

    /// <summary>
    /// The result object that encapsulates the <see cref="EstablishmentFacet"/>
    /// returned by the Establishment search.
    /// </summary>
    public EstablishmentFacets? EstablishmentFacetResults { get; init; }

    /// <summary>
    /// The return status of the call to the <see cref="SearchByKeywordUseCase"/> instance.
    /// </summary>
    public SearchResponseStatus Status { get; }

    /// <summary>
    /// Establishes the status of the underlying search response, i.e. Success or otherwise.
    /// </summary>
    /// <param name="status"></param>
    public SearchByKeywordResponse(SearchResponseStatus status)
    {
        Status = status;
    }

    /// <summary>
    /// The following argument is passed via the constructor and is not changeable
    /// once an instance is created, this ensures we preserve immutability.
    /// </summary>
    /// <param name="establishments">
    /// The readonly collection of <see cref="EstablishmentResults"/>.
    /// </param>
    /// <param name="facetResults">
    /// The readonly collection of <see cref="EstablishmentFacets"/>.
    /// </param>
    /// <param name="status">
    /// The <see cref="SearchResponseStatus"/> of the result of the search.
    /// </param>
    public SearchByKeywordResponse(
        EstablishmentResults establishments,
        EstablishmentFacets facetResults,
        SearchResponseStatus status
        )
    {
        EstablishmentResults = establishments;
        EstablishmentFacetResults = facetResults;
        Status = status;
    }
}
