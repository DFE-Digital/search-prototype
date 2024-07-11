using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mapping;

/// <summary>
/// Facilitates mapping from the received T:Azure.Search.Documents.Models.SearchResults
/// into the required T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments object.
/// </summary>
public sealed class AzureEstablishmentSearchResponseToSearchResultsMapper : IMapper<Response<SearchResults<Establishment>>, EstablishmentResults>
{
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
        var results = input.Value.GetResults(); // synchronous operation TODO is this good enough?

        if (results.Any())
        {
            results.ToList().ForEach(rawSearchResult =>
            {
                if (rawSearchResult.Document == null)
                {
                    throw new InvalidOperationException(
                        "Search result document object cannot be null.");
                }

                // TODO mapper
                establishmentResults.AddEstablishment(
                    new Search.Establishment(
                        urn: rawSearchResult.Document.id ?? string.Empty,
                        name: rawSearchResult.Document.ESTABLISHMENTNAME ?? string.Empty));
            });
        }

        return establishmentResults;
    }
}