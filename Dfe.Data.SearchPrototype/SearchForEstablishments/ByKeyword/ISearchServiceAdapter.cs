using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword;

/// <summary>
/// Describes behaviour for an adaption of core search services infrastructure to allow
/// compatibility with the Dfe.Data.SearchPrototype application search service definition.
/// </summary>
public interface ISearchServiceAdapter
{
    /// <summary>
    /// Describes the required call to the underlying search service infrastructure and the expected
    /// <see cref="SearchResults"/> type to be returned.
    /// </summary>
    /// <param name="searchRequest">
    /// Prescribes the context of the search including the keyword and collection target.
    /// </param>
    /// <returns>
    /// A configured <see cref="SearchResults"/>
    /// object hydrated from the results of the azure search.
    /// </returns>
    Task<SearchResults> SearchAsync(SearchRequest searchRequest);
}
