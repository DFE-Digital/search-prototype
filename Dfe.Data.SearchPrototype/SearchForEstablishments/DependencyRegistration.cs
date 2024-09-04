using Dfe.Data.SearchPrototype.Common.CleanArchitecture.Application.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// Extension method which provides all the pre-registrations required to
/// use the SearchForEstablishments services
/// </summary>
public static class DependencyRegistration
{
    /// <summary>
    /// Register all the necessary SearchForEstablishments services
    /// </summary>
    /// <param name="services"></param>
    public static void AddSearchForEstablishmentServices(this IServiceCollection services)
    {
        services.AddScoped<IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>, SearchByKeywordUseCase>();
    }
}
