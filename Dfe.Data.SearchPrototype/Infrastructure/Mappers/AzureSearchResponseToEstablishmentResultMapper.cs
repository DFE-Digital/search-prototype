﻿using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Search;


namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

/// <summary>
/// Facilitates mapping from the received T:Azure.Search.Documents.Models.SearchResults
/// into the required T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments object.
/// </summary>
public sealed class AzureSearchResponseToEstablishmentResultMapper : IMapper<Response<SearchResults<Establishment>>, EstablishmentResults>
{
    private readonly IMapper<Establishment, Search.Establishment> _azureSearchResultToEstablishmentMapper;

    /// <summary>
    /// The following mapping dependency provides the functionality to map from a raw Azure
    /// search result, to a configured T:Dfe.Data.SearchPrototype.Search.Establishment
    /// instance, the complete implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="azureSearchResultToEstablishmentMapper">
    /// Mapper used to map from the raw Azure search result to a T:Dfe.Data.SearchPrototype.Search.Establishment instance.
    /// </param>
    public AzureSearchResponseToEstablishmentResultMapper(IMapper<Establishment, Search.Establishment> azureSearchResultToEstablishmentMapper)
    {
        _azureSearchResultToEstablishmentMapper = azureSearchResultToEstablishmentMapper;
    }

    /// <summary>
    /// The mapping input is the raw Azure search response T:Azure.Search.Documents.Models.SearchResults
    /// and if any results are contained within the response a new T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments
    /// instance is created, with the responsibility of hydrating this root object and children delegated to the sub-mappers.
    /// </summary>
    /// <param name="input">
    /// A configured T:Azure.Search.Documents.Models.SearchResults instance.
    /// </param>
    /// <returns>
    /// A configured T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments instance.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Exception thrown if an invalid document is derived from the Azure search result.
    /// </exception>
    public EstablishmentResults MapFrom(Response<SearchResults<Establishment>> input)
    {
        ArgumentNullException.ThrowIfNull(input);

        EstablishmentResults establishmentResults = new();
        var results = input.Value.GetResults();

        if (results.Any())
        {
            results.ToList().ForEach(rawSearchResult =>
            {
                if (rawSearchResult.Document == null)
                {
                    throw new InvalidOperationException(
                        "Search result document object cannot be null.");
                }

                establishmentResults.AddEstablishment(
                    establishment: _azureSearchResultToEstablishmentMapper.MapFrom(rawSearchResult.Document));
            });
        }

        return establishmentResults;
    }
}
