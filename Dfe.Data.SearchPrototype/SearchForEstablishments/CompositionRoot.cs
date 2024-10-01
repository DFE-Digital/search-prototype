using Dfe.Data.SearchPrototype.Common.CleanArchitecture.Application.UseCase;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

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
    /// The originating configuration block from which to derive use-case settings.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The exception thrown if no valid <see cref="IServiceCollection"/> is provisioned.
    /// </exception>
    public static void AddSearchForEstablishmentServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services),
                "A service collection is required to configure the search by keyword use-case.");
        }
        services.AddOptions<SearchByKeywordCriteria>()
           .Configure<IConfiguration>(
               (settings, configuration) =>
                   configuration
                       .GetSection(nameof(SearchByKeywordCriteria))
                       .Bind(settings));
        services.AddScoped<IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>, SearchByKeywordUseCase>();
    }
}
