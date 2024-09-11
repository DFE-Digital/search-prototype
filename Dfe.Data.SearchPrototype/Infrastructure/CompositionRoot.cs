using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models = Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure;

/// <summary>
/// The composition root provides a unified location in the application where the composition
/// of the object graphs for the application take place, using the IOC container.
/// </summary>
public static class CompositionRoot
{
    /// <summary>
    /// Extension method which provides all the pre-registrations required to
    /// access the AI azure search service adapter, and perform searches across provisioned indexes.
    /// </summary>
    /// <param name="services">
    /// The originating application services onto which to register the search dependencies.
    /// </param>
    /// <param name="configuration">
    /// The originating configuration block from which to derive search service settings.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The exception thrown if no valid <see cref="IServiceCollection"/> is provisioned.
    /// </exception>
    public static void AddCognitiveSearchAdaptorServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services),
                "A service collection is required to configure the azure AI search adapter dependencies.");
        }

        services.AddOptions<AzureSearchOptions>()
           .Configure<IConfiguration>(
               (settings, configuration) =>
                   configuration
                       .GetSection(nameof(AzureSearchOptions))
                       .Bind(settings));

        services.AddOptions<SearchByKeywordCriteria>()
           .Configure<IConfiguration>(
               (settings, configuration) =>
                   configuration
                       .GetSection(nameof(SearchByKeywordCriteria))
                       .Bind(settings));

        services.AddScoped(typeof(ISearchServiceAdapter), typeof(CognitiveSearchServiceAdapter<SearchResults>));
        services.AddSingleton(typeof(IMapper<Pageable<SearchResult<Models.Establishment>>, Models.EstablishmentResults>), typeof(PageableSearchResultsToEstablishmentResultsMapper));
        services.AddSingleton<IMapper<Dictionary<string, IList<Azure.Search.Documents.Models.FacetResult>>, EstablishmentFacets>, AzureFacetResultToEstablishmentFacetsMapper>();
        services.AddSingleton<IMapper<DataTransferObjects.Establishment, Address>, AzureSearchResultToAddressMapper>();
        services.AddSingleton<IMapper<DataTransferObjects.Establishment, SearchForEstablishments.Models.Establishment>, AzureSearchResultToEstablishmentMapper>();
    }
}