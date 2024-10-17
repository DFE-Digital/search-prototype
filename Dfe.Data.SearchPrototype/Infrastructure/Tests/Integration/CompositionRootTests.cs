using Dfe.Data.SearchPrototype.Infrastructure.Tests.Integration.TestHarness;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Integration;

public class CompositionRootTests : IClassFixture<ConfigBuilder>, IClassFixture<CompositionRootServiceProvider>
{
    private readonly ConfigBuilder _configBuilder;
    private readonly CompositionRootServiceProvider _compositionRootServiceProvider;

    public CompositionRootTests(ConfigBuilder configBuilder, CompositionRootServiceProvider serviceProvider)
    {
        _configBuilder = configBuilder;
        _compositionRootServiceProvider = serviceProvider;
    }

    [Fact]
    public async Task AddCognitiveSearchAdaptorServices_RegistersEverythingNeeded()
    {
        Dictionary<string, string?> config = new() {
            {"AzureSearchOptions:SearchIndex", "establishments" },
            {"AzureSearchOptions:Size", "100"}
        };

        IConfiguration configuration =
            _configBuilder.SetupConfiguration(config);

        IServiceProvider serviceProvider =
            _compositionRootServiceProvider.SetUpServiceProvider(configuration);

        SearchServiceAdapterRequest searchServiceAdapterRequest =
            SearchServiceAdapterRequestTestDouble.Create();

        ISearchServiceAdapter adapter =
            serviceProvider.GetRequiredService<ISearchServiceAdapter>();

        // act
        var response = await adapter.SearchAsync(searchServiceAdapterRequest);
        response.Should().NotBeNull();
    }
}
