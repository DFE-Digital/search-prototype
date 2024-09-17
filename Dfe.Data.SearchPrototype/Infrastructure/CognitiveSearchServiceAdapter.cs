using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Filtering;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Microsoft.Extensions.Options;
using AzureModels = Azure.Search.Documents.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure;

/// <summary>
/// Provides an adaption of the core Azure cognitive search services to allow
/// compatibility with the Dfe.Data.SearchPrototype application search service definition.
/// </summary>
public sealed class CognitiveSearchServiceAdapter<TSearchResult> : ISearchServiceAdapter where TSearchResult : class
{
    private readonly ISearchByKeywordService _searchByKeywordService;
    private readonly IMapper<Pageable<AzureModels.SearchResult<TSearchResult>>, EstablishmentResults> _searchResultMapper;
    private readonly IMapper<Dictionary<string, IList<AzureModels.FacetResult>>, EstablishmentFacets> _facetsMapper;
    private readonly AzureSearchOptions _azureSearchOptions;
    private readonly ISearchFilterExpressionsBuilder _searchFilterExpressionsBuilder;

    /// <summary>
    /// The following dependencies include the core cognitive search service definition,
    /// the complete implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="searchByKeywordService">
    /// Cognitive search (search by keyword) service definition injected via IOC container.
    /// </param>
    /// <param name="azureSearchOptions">
    /// the search options provided through the app-settings
    /// </param>
    /// <param name="searchResultMapper">
    /// Maps the raw Azure search response to the required <see cref="EstablishmentResults"/>
    /// </param>
    /// <param name="facetsMapper">
    /// Maps the raw Azure search response to the required <see cref="EstablishmentFacets"/>
    /// </param>
    /// <param name="searchFilterExpressionsBuilder">
    /// Builds the search filter expression required by Azure AI Search
    /// </param>
    public CognitiveSearchServiceAdapter(
        ISearchByKeywordService searchByKeywordService,
        IOptions<AzureSearchOptions> azureSearchOptions,
        IMapper<Pageable<AzureModels.SearchResult<TSearchResult>>, EstablishmentResults> searchResultMapper,
        IMapper<Dictionary<string, IList<AzureModels.FacetResult>>, EstablishmentFacets> facetsMapper,
        ISearchFilterExpressionsBuilder searchFilterExpressionsBuilder)
    {
        ArgumentNullException.ThrowIfNull(azureSearchOptions.Value);
        _azureSearchOptions = azureSearchOptions.Value;
        _searchByKeywordService = searchByKeywordService;
        _searchResultMapper = searchResultMapper;
        _facetsMapper = facetsMapper;
        _searchFilterExpressionsBuilder = searchFilterExpressionsBuilder;
    }

    /// <summary>
    /// Makes call to underlying azure cognitive search service and uses the prescribed mapper
    /// to adapt the raw Azure search results to the <see cref="SearchResults"/> type.
    /// </summary>
    /// <param name="searchServiceAdapterRequest">
    /// Prescribes the context of the search including the keyword and collection target.
    /// </param>
    /// <returns>
    /// A configured <see cref="SearchResults"/> object hydrated from the results of the azure search.
    /// </returns>
    /// <exception cref="ApplicationException">
    /// An application exception is thrown if we either have no options configured, which
    /// is unrecoverable, or no azure search results are returned which should never be the
    /// case given no matches should return an empty wrapper result object.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Exception thrown if the data cannot be mapped
    /// </exception>
    public async Task<SearchResults> SearchAsync(SearchServiceAdapterRequest searchServiceAdapterRequest)
    {
        SearchOptions searchOptions = new()
        {
            SearchMode = (SearchMode)_azureSearchOptions.SearchMode,
            Size = _azureSearchOptions.Size,
            IncludeTotalCount = _azureSearchOptions.IncludeTotalCount,
        };

        searchServiceAdapterRequest.SearchFields?.ToList()
            .ForEach(searchOptions.SearchFields.Add);

        searchServiceAdapterRequest.Facets?.ToList()
            .ForEach(searchOptions.Facets.Add);

        if (searchServiceAdapterRequest.SearchFilterRequests?.Count > 0)
        {
            searchOptions.Filter = _searchFilterExpressionsBuilder.BuildSearchFilterExpressions(
                searchServiceAdapterRequest.SearchFilterRequests.ToList()
                    .Select(x => new SearchFilterRequest(x.Key, x.Value)));
        }

        Response<SearchResults<TSearchResult>> searchResults =
            await _searchByKeywordService.SearchAsync<TSearchResult>(
                searchServiceAdapterRequest.SearchKeyword,
                _azureSearchOptions.SearchIndex,
                searchOptions
            )
            .ConfigureAwait(false) ??
                throw new ApplicationException(
                    $"Unable to derive search results based on input {searchServiceAdapterRequest.SearchKeyword}.");

        var results = new SearchResults()
        {
            Establishments = _searchResultMapper.MapFrom(searchResults.Value.GetResults()),
            Facets = searchResults.Value.Facets != null
                ? _facetsMapper.MapFrom(searchResults.Value.Facets.ToDictionary())
                : null
        };

        return results;
    }
}
