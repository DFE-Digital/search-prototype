using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// The search criteria used by the <see cref="SearchByKeywordUseCase"/>
/// which is set using the configuration settings defined (via IOptions pattern).
/// </summary>
public class SearchByKeywordCriteria
{
    /// <summary>
    /// The collection of fields in the underlying collection to search over.
    /// </summary>
    public IList<string> SearchFields { get; set; } = [];

    /// <summary>
    /// The collection of facets to apply in the search request.
    /// </summary>
    public IList<string> Facets { get; set; } = [];
}
