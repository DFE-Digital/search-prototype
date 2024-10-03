using Dfe.Data.SearchPrototype.Infrastructure;
using Dfe.Data.SearchPrototype.Infrastructure.DataTransferObjects;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;
using Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.ByKeyword.TestDoubles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Dfe.Data.SearchPrototype.Tests.Integration.TestHarness;

public class CompositionRootServiceProvider
{
    public IServiceProvider SetUpServiceProvider(IConfiguration config)
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(config);

        services.AddSearchForEstablishmentServices(config);

        services.AddScoped<ISearchServiceAdapter>(provider => SearchServiceAdapterTestDouble.MockFor(SearchResultsTestDouble.CreateWithNoResults()));
        return services.BuildServiceProvider();
    }
}
