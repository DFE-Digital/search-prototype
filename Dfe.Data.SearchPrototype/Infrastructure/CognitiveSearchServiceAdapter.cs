using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Search.Application.Adapters;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using DfE.Data.ComponentLibrary.Infrastructure.CognitiveSearch.Search;

namespace Dfe.Data.SearchPrototype.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CognitiveSearchServiceAdapter : ISearchServiceAdapter
    {
        private readonly ISearchService _cognitiveSearchService;
        private readonly ISearchOptionsFactory _searchOptionsFactory;
        private readonly IMapper<Response<SearchResults<object>>, Establishments> _searchResponseMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cognitiveSearchService"></param>
        /// <param name="searchOptionsFactory"></param>
        /// <param name="searchResponseMapper"></param>
        public CognitiveSearchServiceAdapter(
            ISearchService cognitiveSearchService,
            ISearchOptionsFactory searchOptionsFactory,
            IMapper<Response<SearchResults<object>>, Establishments> searchResponseMapper)
        {
            _searchOptionsFactory = searchOptionsFactory;
            _cognitiveSearchService = cognitiveSearchService;
            _searchResponseMapper = searchResponseMapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchContext"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<Establishments> Search(SearchContext searchContext)
        {
            SearchOptions searchOptions =
                _searchOptionsFactory.GetSearchOptions(searchContext.TargetCollection) ??
                throw new ApplicationException(
                    $"Search options cannot be derived for {searchContext.TargetCollection}.");

            Response<SearchResults<object>> searchResults =
                await _cognitiveSearchService.SearchAsync<object>(
                    searchContext.SearchKeyword,
                    searchContext.TargetCollection,
                    searchOptions
                )
                .ConfigureAwait(false) ??
                    throw new ApplicationException(
                        $"Unable to derive search results based on input {searchContext.SearchKeyword}.");

            return _searchResponseMapper.MapFrom(searchResults);
        }
    }
}
