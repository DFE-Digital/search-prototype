using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// The object that encapsulates a single facet result for a facet 
/// </summary>
public class FacetResult
{
    /// <summary>
    /// The value of the facet result
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// The number of records that belong to this facet value
    /// </summary>
    public int? Count { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="value">The value of the facet result</param>
    /// <param name="count">The number of records that belong to this facet value</param>
    public FacetResult(string value, int? count)
    {
        Value = value;
        Count = count;
    }
}

/// <summary>
/// The object that encapsulates the Faceted results returned by the
/// T:Dfe.Data.SearchPrototype.SearchForEstablishments.SearchByKeywordUseCase instance
/// </summary>
public class EstablishmentFacet
{
    /// <summary>
    /// The facet (field) name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The collection of T:Dfe.Data.SearchPrototype.Search.FacetResult
    /// </summary>
    public IList<FacetResult> Results { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="facetName"></param>
    /// <param name="facetResults"></param>
    public EstablishmentFacet(string facetName, IList<FacetResult> facetResults)
    {
        Name = facetName;
        Results = facetResults;
    }
}

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
