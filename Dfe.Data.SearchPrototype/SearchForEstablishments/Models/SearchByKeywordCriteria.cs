namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// The search criteria use by the <see cref="SearchByKeywordUseCase"/>
/// which is set using the IOptions interface
/// </summary>
public class SearchByKeywordCriteria
{
    /// <summary>
    /// The fields to search over
    /// </summary>
    public IList<string> SearchFields { get; set; } = new List<string>();
    /// <summary>
    /// The facets to request in the search request
    /// </summary>
    public IList<string> Facets { get; set; } = new List<string>();
}
