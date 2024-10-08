using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Filtering;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;

namespace Dfe.Data.SearchPrototype.Infrastructure.Builders
{
    /// <summary>
    /// Provides a concrete implementation of the <see cref="ISearchOptionsBuilder"/> abstraction,
    /// which establishes a configured <see cref="SearchOptions" /> instance which conforms to the prescribed behaviour.
    /// </summary>
    public sealed class SearchOptionsBuilder : ISearchOptionsBuilder
    {
        private readonly SearchOptions _searchOptions;
        private readonly ISearchFilterExpressionsBuilder? _searchFilterExpressionsBuilder;

        private SearchMode? _searchMode;
        private int? _size;
        private bool? _includeTotalCount;
        private IList<string>? _searchFields;
        private IList<string>? _facets;
        private IList<FilterRequest>? _filters;

        /// <summary>
        /// The following <see cref="ISearchFilterExpressionsBuilder"/> dependency provides the
        /// behaviour on which to generate fully configured, search filter string expressions based
        /// on the provisioned request, the complete implementation of which is defined in the IOC container.
        /// </summary>
        /// <param name="searchFilterExpressionsBuilder">
        /// Builds the search filter expression required by Azure AI Search
        /// </param>
        public SearchOptionsBuilder(ISearchFilterExpressionsBuilder? searchFilterExpressionsBuilder = null)
        {
            if (searchFilterExpressionsBuilder != null){
                _searchFilterExpressionsBuilder = searchFilterExpressionsBuilder;
            }

            _searchOptions = new SearchOptions();
        }

        /// <summary>
        /// Sets the number of search items to retrieve.
        /// </summary>
        /// <param name="size">
        /// The number of results to return as specified.
        /// </param>
        /// <returns>
        /// The updated builder instance.
        /// </returns>
        public ISearchOptionsBuilder WithSize(int? size)
        {
            _size = size;
            return this;
        }

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
        public ISearchOptionsBuilder WithSearchMode(SearchMode searchMode)
        {
            _searchMode = searchMode;
            return this;
        }

        /// <summary>
        /// Sets the option to include the total search results count in the search response.
        /// </summary>
        /// <param name="includeTotalCount">
        /// The boolean value used to instruct the total count to be added to the response, or otherwise.
        /// </param>
        /// <returns>
        /// The updated builder instance.
        /// </returns>
        public ISearchOptionsBuilder WithIncludeTotalCount(bool? includeTotalCount)
        {
            _includeTotalCount = includeTotalCount;
            return this;
        }

        /// <summary>
        /// Sets the fields on which to establish the search.
        /// </summary>
        /// <param name="searchFields">
        /// List of fields over which to specify the search.
        /// </param>
        /// <returns>
        /// The updated builder instance.
        /// </returns>
        public ISearchOptionsBuilder WithSearchFields(IList<string>? searchFields)
        {
            _searchFields = searchFields;
            return this;
        }

        /// <summary>
        /// Sets the facets to include in the search response.
        /// </summary>
        /// <param name="facets">
        /// List of facets to include in the search response.
        /// </param>
        /// <returns>
        /// The updated builder instance.
        /// </returns>
        public ISearchOptionsBuilder WithFacets(IList<string>? facets)
        {
            _facets = facets;
            return this;
        }

        /// <summary>
        /// Sets the filters on which to establish the search.
        /// </summary>
        /// <param name="filters">
        /// List of filters on which to establish the search.
        /// </param>
        /// <returns>
        /// The updated builder instance.
        /// </returns>
        public ISearchOptionsBuilder WithFilters(IList<FilterRequest>? filters)
        {
            _filters = filters;
            return this;
        }

        /// <summary>
        /// Builds the configured instance of the <see cref="SearchOptions"/> type requested.
        /// </summary>
        /// <returns>
        /// The fully configured (built) <see cref="SearchOptions"/> instance.
        /// </returns>
        public SearchOptions Build()
        {
            _searchOptions.SearchMode = _searchMode;
            _searchOptions.Size = _size;
            _searchOptions.IncludeTotalCount = _includeTotalCount;
            _searchFields?.ToList().ForEach(_searchOptions.SearchFields.Add);
            _facets?.ToList().ForEach(_searchOptions.Facets.Add);

            if (_filters?.Count > 0 && _searchFilterExpressionsBuilder != null)
            {
                _searchOptions.Filter =
                    _searchFilterExpressionsBuilder.BuildSearchFilterExpressions(
                        _filters.Select(filterRequest =>
                            new SearchFilterRequest(filterRequest.FilterName, filterRequest.FilterValues)));
            }

            return _searchOptions;
        }
    }
}
