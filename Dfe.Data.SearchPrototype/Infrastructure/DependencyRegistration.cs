using Azure.Search.Documents.Models;
using Azure;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Microsoft.Extensions.DependencyInjection;
using Azure.Search.Documents;
using Dfe.Data.SearchPrototype.Infrastructure.Options.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Options;

namespace Dfe.Data.SearchPrototype.Infrastructure;

/// <summary>
/// Extension method which provides all the pre-registrations required to
/// access services to adapt the Dfe.Data.Common.Infrastructure.CognitiveSearch infrastructure
/// to SearchPrototype application layer
/// </summary>
public static class DependencyRegistration
{
    /// <summary>
    /// Register the necessary infrastucture adaptor services
    /// </summary>
    /// <param name="services"></param>
    public static void AddCognitiveSearchAdaptorServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ISearchServiceAdapter), typeof(CognitiveSearchServiceAdapter<Establishment>));
        services.AddSingleton(typeof(IMapper<Pageable<SearchResult<Establishment>>, EstablishmentResults>), typeof(PageableSearchResultsToEstablishmentResultsMapper));
        services.AddSingleton<IMapper<SearchSettingsOptions, SearchOptions>, SearchOptionsToAzureOptionsMapper>();
        services.AddSingleton<IMapper<Establishment, Address>, AzureSearchResultToAddressMapper>();
        services.AddSingleton<IMapper<Establishment, SearchForEstablishments.Models.Establishment>, AzureSearchResultToEstablishmentMapper>();
        services.AddScoped<ISearchOptionsFactory, SearchOptionsFactory>();
    }
}

