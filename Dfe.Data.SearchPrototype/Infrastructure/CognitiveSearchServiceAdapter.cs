﻿using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure;

/// <summary>
/// Provides an adaption of the core Azure cognitive search services to allow
/// compatibility with the Dfe.Data.SearchPrototype application search service definition.
/// </summary>
public sealed class CognitiveSearchServiceAdapter<TSearchResult> : ISearchServiceAdapter where TSearchResult : class
{
    private readonly ISearchByKeywordService _searchByKeywordService;
    private readonly ISearchOptionsFactory _searchOptionsFactory;
    private readonly IMapper<Response<SearchResults<TSearchResult>>, EstablishmentResults> _searchResponseMapper;

    /// <summary>
    /// The following dependencies include the core cognitive search service definition,
    /// the complete implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="searchByKeywordService">
    /// Cognitive search (search by keyword) service definition injected via IOC container.
    /// </param>
    /// <param name="searchOptionsFactory">
    /// Factory class definition for prescribing the requested search options (by collection context).
    /// </param>
    /// <param name="searchResponseMapper">
    /// Maps the raw azure search response to the required "T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments"
    /// </param>
    public CognitiveSearchServiceAdapter(
        ISearchByKeywordService searchByKeywordService,
        ISearchOptionsFactory searchOptionsFactory,
        IMapper<Response<SearchResults<TSearchResult>>, EstablishmentResults> searchResponseMapper)
    {
        _searchOptionsFactory = searchOptionsFactory;
        _searchByKeywordService = searchByKeywordService;
        _searchResponseMapper = searchResponseMapper;
    }

    /// <summary>
    /// Makes call to underlying azure cognitive search service and uses the prescribed mapper
    /// to adapt the raw Azure search results to the "T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments" type.
    /// </summary>
    /// <param name="searchContext">
    /// Prescribes the context of the search including the keyword and collection target.
    /// </param>
    /// <returns>
    /// A configured "T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments"
    /// object hydrated from the results of the azure search.
    /// </returns>
    /// <exception cref="ApplicationException">
    /// An application exception is thrown if we either have no options configured, which
    /// is unrecoverable, or no azure search results are returned which should never be the
    /// case given no matches should return an empty wrapper result object.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Exception thrown if the data cannot be mapped
    /// </exception>

    public async Task<EstablishmentResults> SearchAsync(SearchContext searchContext)
    {
        SearchOptions searchOptions =
            _searchOptionsFactory.GetSearchOptions(searchContext.TargetCollection) ??
            throw new ApplicationException(
                $"Search options cannot be derived for {searchContext.TargetCollection}.");

        Response<SearchResults<TSearchResult>> searchResults =
            await _searchByKeywordService.SearchAsync<TSearchResult>(
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
