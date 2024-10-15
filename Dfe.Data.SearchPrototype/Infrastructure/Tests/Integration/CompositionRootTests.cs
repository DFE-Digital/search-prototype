using Dfe.Data.SearchPrototype.Infrastructure.Tests.Integration.TestHarness;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Integration;

public class CompositionRootTests :IClassFixture<ConfigBuilder>, IClassFixture<CompositionRootServiceProvider>
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

    [Fact]
    public void AddCognitiveSearchAdaptorServices_MissingAzureSearchOption_Size_ThrowOptionsValidationException()
    {
        Dictionary<string, string?> config = new() {
            {"AzureSearchOptions:SearchIndex", "establishments" },
        };

        IConfiguration configuration =
            _configBuilder.SetupConfiguration(config);

        IServiceProvider serviceProvider =
            _compositionRootServiceProvider.SetUpServiceProvider(configuration);

        // act, assert
        OptionsValidationException exception =
             Assert.Throws<OptionsValidationException>(() =>
                 serviceProvider.GetRequiredService<ISearchServiceAdapter>());

        Assert.Equal("DataAnnotation validation failed for 'AzureSearchOptions' members: 'Size' with the error: 'Size must be greater than zero.'.", exception.Message);
    }

    [Fact]
    public void AddCognitiveSearchAdaptorServices_MissingAzureSearchOption_SearchIndex_ThrowOptionsValidationException()
    {
        Dictionary<string, string?> config = new() {
            {"AzureSearchOptions:Size", "100"}
        };

        IConfiguration configuration =
            _configBuilder.SetupConfiguration(config);

        IServiceProvider serviceProvider =
            _compositionRootServiceProvider.SetUpServiceProvider(configuration);

        // act, assert
        OptionsValidationException exception =
             Assert.Throws<OptionsValidationException>(() =>
                 serviceProvider.GetRequiredService<ISearchServiceAdapter>());

        Assert.Equal("DataAnnotation validation failed for 'AzureSearchOptions' members: 'SearchIndex' with the error: 'The SearchIndex field is required.'.", exception.Message);
    }

    [Fact]
    public void AddCognitiveSearchAdaptorServices_IncorrectAzureSearchOption_SearchMode_ThrowOptionsValidationException()
    {
        Dictionary<string, string?> config = new() {
            {"AzureSearchOptions:SearchIndex", "establishments" },
            {"AzureSearchOptions:Size", "100"},
            {"AzureSearchOptions:SearchMode", "3"},
        };

        IConfiguration configuration =
            _configBuilder.SetupConfiguration(config);

        IServiceProvider serviceProvider =
            _compositionRootServiceProvider.SetUpServiceProvider(configuration);

        // act, assert
        OptionsValidationException exception =
             Assert.Throws<OptionsValidationException>(() =>
                 serviceProvider.GetRequiredService<ISearchServiceAdapter>());

        Assert.Equal("DataAnnotation validation failed for 'AzureSearchOptions' members: 'SearchMode' with the error: 'Search Mode must be 0 (Any search terms may match) or 1 (All search terms must match) only.'.", exception.Message);
    }
}
