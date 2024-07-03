using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Search.Application.Adapters;
using Dfe.Data.SearchPrototype.Search.Domain;
using DfE.Data.ComponentLibrary.Infrastructure.CognitiveSearch.Search;

namespace Dfe.Data.SearchPrototype.Infrastructure
{
    public sealed class CognitiveSearchServiceAdapter : ISearchServiceAdapter
    {
        private readonly ISearchService _cognitiveSearchService;
        private readonly ISearchSettingsOptionsFactory _searchSettingsOptionsFactory;

        public CognitiveSearchServiceAdapter(
            ISearchService cognitiveSearchService,
            ISearchSettingsOptionsFactory searchSettingsOptionsFactory)
        {
            _searchSettingsOptionsFactory = searchSettingsOptionsFactory;
            _cognitiveSearchService = cognitiveSearchService;
        }

        public async Task<SearchResults> Search(SearchContext searchContext)
        {
            var searchSettingsOptions =
                _searchSettingsOptionsFactory.GetSearchOptions(searchContext) ??
                throw new ApplicationException(
                    $"Search options cannot be derived for {searchContext.SearchTarget}.");

            var searchOptions = searchSettingsOptions.MapToSearchOptions();

            var searchResults =
                await _cognitiveSearchService.SearchAsync<object>(
                    searchContext.SearchKeyword,
                    searchSettingsOptions.SearchIndex,
                    searchOptions
                )
                .ConfigureAwait(false) ??
                    throw new ApplicationException(
                        $"Unable to derive search results based on input {searchContext.SearchKeyword}.");

            return new SearchResponse(
                searchResults.Value, searchResults.Value.Facets);
        }
    }
}
