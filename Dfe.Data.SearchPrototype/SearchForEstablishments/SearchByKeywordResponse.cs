using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// This is the object that carries the response (output) back from the
/// T:Dfe.Data.SearchPrototype.SearchForEstablishments.SearchByKeywordUseCase instance.
/// The response will encapsulate any search results found along with a status.
/// </summary>
public sealed class SearchByKeywordResponse
{
    /// <summary>
    /// The readonly collection of T:Dfe.Data.SearchPrototype.Search.Establishment search results.
    /// </summary>
    public IReadOnlyCollection<Establishment> EstablishmentResults { get;}
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
    public SearchByKeywordResponse(IReadOnlyCollection<Establishment> establishments)
    {
        EstablishmentResults = establishments;
    }
}
