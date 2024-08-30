using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// This is the object that carries the response (output) back from the
/// T:Dfe.Data.SearchPrototype.SearchForEstablishments.SearchByKeywordUseCase instance.
/// </summary>
public sealed class SearchByKeywordResponse
{
    /// <summary>
    /// The readonly collection of T:Dfe.Data.SearchPrototype.Search.Establishment search results.
    /// </summary>
    public IReadOnlyCollection<Establishment> EstablishmentResults { get;}

    /// <summary>
    /// The readonly collection of T:Dfe.Data.SearchPrototype.Search.EstablishmentFacet returned by the Establishment search
    /// </summary>
    public IReadOnlyCollection<EstablishmentFacet>? EstablishmentFacetResults { get; }

    /// <summary>
    /// The return status of the call to the
    /// T:Dfe.Data.SearchPrototype.SearchForEstablishments.SearchByKeywordUseCase instance
    /// </summary>
    public SearchResponseStatus Status { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public SearchByKeywordResponse()
    {
        EstablishmentResults = new List<Establishment>();
    }

    /// <summary>
    /// The following argument is passed via the constructor and is not changeable
    /// once an instance is created, this ensures we preserve immutability.
    /// </summary>
    /// <param name="establishments">
    /// The readonly collection of T:Dfe.Data.SearchPrototype.Search.Establishment search results.
    /// </param>
    /// <param name="facetResults">
    /// The readonly collection of T:Dfe.Data.SearchPrototype.Search.EstablishmentFacet
    /// </param>
    public SearchByKeywordResponse(IReadOnlyCollection<Establishment> establishments, IReadOnlyCollection<EstablishmentFacet>? facetResults = null)
    {
        EstablishmentResults = establishments;
        EstablishmentFacetResults = facetResults;
    }
}
