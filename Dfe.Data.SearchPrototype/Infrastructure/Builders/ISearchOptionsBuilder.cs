using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;

namespace Dfe.Data.SearchPrototype.Infrastructure.Builders
{
    /// <summary>
    /// Provides an abstraction on which to establish the behaviour
    /// used to build configured <see cref="SearchOptions" /> instances.
    /// </summary>
    public interface ISearchOptionsBuilder
    {
        /// <summary>
        /// Sets the number of search items to retrieve.
        /// </summary>
        /// <param name="size">
        /// The number of results to return as specified.
        /// </param>
        /// <returns>
        /// The updated builder instance.
        /// </returns>
        ISearchOptionsBuilder WithSize(int? size);

        /// <summary>
        /// Sets the value used to define how many
        /// records are skipped in the search response (if any).
        /// </summary>
        /// <param name="offset">
        /// The number of initial search results to skip.
        /// </param>
        /// <returns>
        /// The updated builder instance.
        /// </returns>
        ISearchOptionsBuilder WithOffset(int? offset);

        /// <summary>
        /// Sets the mode of search to invoke, i.e. All or Any.
        /// </summary>
        /// <param name="searchMode">
        /// The mode of search to invoke, i.e. any search terms may match (Any),
        /// or all search terms must match (All).
        /// </param>
        /// <returns>
        /// The updated builder instance.
        /// </returns>
        ISearchOptionsBuilder WithSearchMode(SearchMode searchMode);

        /// <summary>
        /// Sets the option to include the total search results count in the search response.
        /// </summary>
        /// <param name="includeTotalCount">
        /// The boolean value used to instruct the total count to be added to the response, or otherwise.
        /// </param>
        /// <returns>
        /// The updated builder instance.
        /// </returns>
        ISearchOptionsBuilder WithIncludeTotalCount(bool? includeTotalCount);

        /// <summary>
        /// Sets the fields on which to establish the search.
        /// </summary>
        /// <param name="searchFields">
        /// List of fields over which to specify the search.
        /// </param>
        /// <returns>
        /// The updated builder instance.
        /// </returns>
        ISearchOptionsBuilder WithSearchFields(IList<string>? searchFields);

        /// <summary>
        /// Sets the facets to include in the search response.
        /// </summary>
        /// <param name="facets">
        /// List of facets to include in the search response.
        /// </param>
        /// <returns>
        /// The updated builder instance.
        /// </returns>
        ISearchOptionsBuilder WithFacets(IList<string>? facets);

        /// <summary>
        /// Sets the filters on which to establish the search.
        /// </summary>
        /// <param name="filters">
        /// List of filters on which to establish the search.
        /// </param>
        /// <returns>
        /// The updated builder instance.
        /// </returns>
        ISearchOptionsBuilder WithFilters(IList<FilterRequest>? filters);

        /// <summary>
        /// Builds the configured instance of the <see cref="SearchOptions"/> type requested.
        /// </summary>
        /// <returns>
        /// The fully configured (built) <see cref="SearchOptions"/> instance.
        /// </returns>
        SearchOptions Build();
    }
}
