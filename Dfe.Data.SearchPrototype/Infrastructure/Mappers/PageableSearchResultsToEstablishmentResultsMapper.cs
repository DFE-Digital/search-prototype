using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.DataTransferObjects;
using Models = Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

/// <summary>
/// Facilitates mapping from the received <see cref="Models.SearchResults"/> 
/// into the required <see cref="Models.EstablishmentResults"/>  object.
/// </summary>
public sealed class PageableSearchResultsToEstablishmentResultsMapper : IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, Models.EstablishmentResults>
{
    private readonly IMapper<DataTransferObjects.Establishment, Models.Establishment> _azureSearchResultToEstablishmentMapper;

    /// <summary>
    /// The following mapping dependency provides the functionality to map from a raw Azure
    /// search result, to a configured <see cref="Establishment"/>
    /// instance, the complete implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="azureSearchResultToEstablishmentMapper">
    /// Mapper used to map from the raw Azure search result to a <see cref="Establishment"/> instance.
    /// </param>
    public PageableSearchResultsToEstablishmentResultsMapper(IMapper<DataTransferObjects.Establishment, Models.Establishment> azureSearchResultToEstablishmentMapper)
    {
        _azureSearchResultToEstablishmentMapper = azureSearchResultToEstablishmentMapper;
    }

    /// <summary>
    /// The mapping input is the raw Azure search response <see cref="Models.SearchResults"/>
    /// and if any results are contained within the response a new Dfe.Data.SearchPrototype.Search.Establishments
    /// instance is created, with the responsibility of hydrating this root object and children delegated to the sub-mappers.
    /// </summary>
    /// <param name="input">
    /// A configured <see cref="Models.SearchResults"/> instance.
    /// </param>
    /// <returns>
    /// A configured <see cref="Models.EstablishmentResults"/> instance.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Exception thrown if an invalid document is derived from the Azure search result.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Exception thrown if the data cannot be mapped
    /// </exception>
    public Models.EstablishmentResults MapFrom(Pageable<SearchResult<DataTransferObjects.Establishment>> input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.Any())
        {
            var mappedResults = input.Select(result =>
                 result.Document != null
                    ? _azureSearchResultToEstablishmentMapper.MapFrom(result.Document)
                    : throw new InvalidOperationException(
                        "Search result document object cannot be null.")
                );
            return new Models.EstablishmentResults(mappedResults);
        }
        else
        {
            return new Models.EstablishmentResults();
        }
    }
}
