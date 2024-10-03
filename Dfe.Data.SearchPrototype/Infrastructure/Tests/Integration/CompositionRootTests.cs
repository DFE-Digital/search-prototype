using Dfe.Data.SearchPrototype.Infrastructure.Tests.Integration.TestHarness;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Integration;

public class CompositionRootTests :IClassFixture<ConfigBuilder>, IClassFixture<CompositionRootServiceProvider>
{
    private IConfiguration _configuration;
    private IServiceProvider _serviceProvider;

    private static Dictionary<string, string?> InMemoryConfig => new() {
            {"AzureSearchOptions:SearchIndex", "establishments" }
        };

    public CompositionRootTests(ConfigBuilder configBuilder, CompositionRootServiceProvider serviceProvider)
    {
        _configuration = configBuilder.SetupConfiguration(InMemoryConfig);
        _serviceProvider = serviceProvider.SetUpServiceProvider(_configuration);
    }

    [Fact]
    public async Task AddCognitiveSearchAdaptorServices_RegistersEverythingNeeded()
    {
        var adapter = _serviceProvider.GetRequiredService<ISearchServiceAdapter>();

        var searchServiceAdapterRequest = SearchServiceAdapterRequestTestDouble.Create();

        // act
        var response = await adapter.SearchAsync(searchServiceAdapterRequest);
        response.Should().NotBeNull();
    }
}
