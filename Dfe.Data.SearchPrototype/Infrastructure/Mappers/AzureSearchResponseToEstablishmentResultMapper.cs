using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

/// <summary>
/// Facilitates mapping from the received T:Azure.Search.Documents.Models.SearchResults
/// into the required T:Dfe.Data.SearchPrototype.Search.EstablishmentResults object.
/// </summary>
public sealed class AzureSearchResponseToEstablishmentResultMapper : IMapper<Response<SearchResults<Establishment>>, EstablishmentResults>
{
    private readonly IMapper<Establishment, SearchForEstablishments.Models.Establishment> _azureSearchResultToEstablishmentMapper;

    /// <summary>
    /// The following mapping dependency provides the functionality to map from a raw Azure
    /// search result, to a configured T:Dfe.Data.SearchPrototype.Search.Establishment
    /// instance, the complete implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="azureSearchResultToEstablishmentMapper">
    /// Mapper used to map from the raw Azure search result to a T:Dfe.Data.SearchPrototype.Search.Establishment instance.
    /// </param>
    public AzureSearchResponseToEstablishmentResultMapper(IMapper<Establishment, SearchForEstablishments.Models.Establishment> azureSearchResultToEstablishmentMapper)
    {
        _azureSearchResultToEstablishmentMapper = azureSearchResultToEstablishmentMapper;
    }

    /// <summary>
    /// The mapping input is the raw Azure search response T:Azure.Search.Documents.Models.SearchResults
    /// and if any results are contained within the response a new Dfe.Data.SearchPrototype.Search.Establishments
    /// instance is created, with the responsibility of hydrating this root object and children delegated to the sub-mappers.
    /// </summary>
    /// <param name="input">
    /// A configured T:Azure.Search.Documents.Models.SearchResults instance.
    /// </param>
    /// <returns>
    /// A configured T:Dfe.Data.SearchPrototype.Search.EstablishmentResults instance.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Exception thrown if an invalid document is derived from the Azure search result.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Exception thrown if the data cannot be mapped
    /// </exception>
    public EstablishmentResults MapFrom(Response<SearchResults<Establishment>> input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var results = input.Value.GetResults();
        if (results.Any())
        {
            var mappedResults = results.Select(result =>
                 result.Document != null
                    ? _azureSearchResultToEstablishmentMapper.MapFrom(result.Document)
                    : throw new InvalidOperationException(
                        "Search result document object cannot be null.")
                );
            return new EstablishmentResults(mappedResults);
        }
        else
        {
            return new EstablishmentResults();
        }
    }
}
