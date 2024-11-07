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
    /// The Total Count returned from Establishment search gives us a total
    /// of all available records which correlates with the given search criteria.
    /// </summary>
    public int TotalNumberOfEstablishments { get; init; }

    /// <summary>
    /// Establishes the status of the underlying search response, i.e. Success or otherwise.
    /// </summary>
    /// <param name="status"></param>
    public SearchByKeywordResponse(SearchResponseStatus status)
    {
        Status = status;
    }
}
