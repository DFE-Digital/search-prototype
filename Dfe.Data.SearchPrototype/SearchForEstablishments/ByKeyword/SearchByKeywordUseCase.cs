using Dfe.Data.SearchPrototype.Common.CleanArchitecture.Application.UseCase;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Microsoft.Extensions.Options;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword;

/// <summary>
/// This use case is responsible for handling keyword search requests. The use case will delegate responsibility
/// for the underlying search mechanics to the prescribed concrete T:Dfe.Data.SearchPrototype.Search.ISearchServiceAdapter
/// instance. Use case will be responsible for managing this work-flow and ensuring the consumer is responded to with
/// a conditioned response, related to the status of the work-flow on completion.
/// </summary>
public sealed class SearchByKeywordUseCase : IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>
{
    private readonly ISearchServiceAdapter _searchServiceAdapter;
    private readonly SearchByKeywordCriteria _searchByKeywordCriteriaOptions;

    /// <summary>
    /// The following dependencies include the core cognitive search service definition,
    /// the complete implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="searchServiceAdapter">
    /// The concrete  implementation of the T:Dfe.Data.SearchPrototype.Search.ISearchServiceAdapter
    /// defined within, and injected by the IOC container.
    /// </param>
    /// <param name="searchByKeywordCriteriaOptions">
    /// The <see cref="SearchByKeywordCriteria"/> define the search fields and facets on
    /// which to conduct the underlying search. This is defined in configuration using
    /// the options pattern as follows,
    /// <code>
    /// "SearchByKeywordCriteria": {
    ///     "SearchFields": [
    ///         "ESTABLISHMENTNAME",
    ///         "TOWN",
    ///         "PHASEOFEDUCATION"
    ///     ],
    ///     "Facets": [
    ///     "PHASEOFEDUCATION",
    ///     "ESTABLISHMENTSTATUSNAME"
    ///     ]
    /// }
    /// </code>
    /// </param>
    public SearchByKeywordUseCase(
        ISearchServiceAdapter searchServiceAdapter,
        IOptions<SearchByKeywordCriteria> searchByKeywordCriteriaOptions)
    {
        ArgumentNullException.ThrowIfNull(searchByKeywordCriteriaOptions);
        _searchByKeywordCriteriaOptions = searchByKeywordCriteriaOptions.Value;
        _searchServiceAdapter = searchServiceAdapter;
    }

    /// <summary>
    /// Handler for search by keyword requests which is responsible for orchestrating the 
    /// work-flow associated with the required search, and composing a response based on the
    /// status of the completed work-flow.
    /// </summary>
    /// <param name="request">
    /// The <see cref="SearchByKeywordRequest" /> parameter is the object used
    /// to allow requests (send input) through to the use-case (i.e. acts as an input port).
    /// </param>
    /// <returns>
    /// The <see cref="SearchByKeywordResponse"/> output parameter is the object used
    /// to encapsulate the response from the use-case (send output) from the use-case (i.e. acts as an output port).
    /// </returns>
    public async Task<SearchByKeywordResponse> HandleRequest(SearchByKeywordRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.SearchKeyword))
        {
            return new SearchByKeywordResponse(SearchResponseStatus.InvalidRequest);
        }

        try
        {
            SearchResults results =
                await _searchServiceAdapter.SearchAsync(
                    new SearchRequest(
                        request.SearchKeyword,
                        _searchByKeywordCriteriaOptions.SearchFields,
                        _searchByKeywordCriteriaOptions.Facets));

            return results switch
            {
                null => new(status: SearchResponseStatus.SearchServiceError),
                _ => new(status: SearchResponseStatus.Success)
                {
                    EstablishmentResults = results.Establishments,
                    EstablishmentFacetResults = results.Facets
                }
            };
        }
        catch (Exception) // something went wrong in the infrastructure tier.
        {
            return new(status: SearchResponseStatus.SearchServiceError);
        }
    }
}
