using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Filtering;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.Shared;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Runtime.CompilerServices;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Integration;

public class CognitiveSearchServiceAdaptorIntegrationTests
{
    private AzureSearchOptions _options = AzureSearchOptionsTestDouble.Stub();

    private static Dictionary<string, string> InMemoryConfig => new Dictionary<string, string> {
            //{"FilterKeyToFilterExpressionMapOptions:FilterChainingLogicalOperator", "AndLogicalOperator"},
            //{"FilterKeyToFilterExpressionMapOptions:SearchFilterToExpressionMap:PHASEOFEDUCATION", "SearchInFilterExpression" },
            //{"FilterKeyToFilterExpressionMapOptions:SearchFilterToExpressionMap:ESTABLISHMENTSTATUSNAME", "SearchInFilterExpression" }
        };

    [Fact]
    public async Task  CompositionRoot_DoesEverythingItNeeds()
    {
        // configuration
        IConfiguration config = SetupConfiguration(InMemoryConfig);

        // services
        var serviceProvider = SetUpServiceProvider(config);

        // Create system under test
        var adapter = new CognitiveSearchServiceAdapter<DataTransferObjects.Establishment>(
            serviceProvider.GetRequiredService<ISearchByKeywordService>(),
            IOptionsTestDouble.IOptionsMockFor(_options),
            serviceProvider.GetRequiredService<IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults>>(),
            serviceProvider.GetRequiredService<IMapper<Dictionary<string, IList<Azure.Search.Documents.Models.FacetResult>>, EstablishmentFacets>>(),
            serviceProvider.GetRequiredService<ISearchFilterExpressionsBuilder>()
            );

        var searchServiceAdapterRequest = SearchServiceAdapterRequestTestDouble.Create();

        // act
        var response = await adapter.SearchAsync(searchServiceAdapterRequest);
    }

    private IServiceProvider SetUpServiceProvider(IConfiguration config)
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(config);
        services.AddCognitiveSearchAdaptorServices(config);
        //services.AddDefaultSearchFilterServices(config);

        // replace Common.Infrastructure services with mocks
        services.RemoveAll<ISearchByKeywordService>();
        var mockSearchService = new SearchServiceMockBuilder()
            .WithSearchKeywordAndCollection("searchKeyword","establishments")
            .WithSearchResults(new SearchResultFakeBuilder()
                .WithSearchResults()
                .Create())
            .Create();
        services.AddScoped<ISearchByKeywordService>(provider => mockSearchService);

        services.RemoveAll<ISearchFilterExpressionsBuilder>();
        var mockFilterExpressionBuilder = new FilterExpressionBuilderTestDouble().Create();
        services.AddScoped<ISearchFilterExpressionsBuilder>(provider => mockFilterExpressionBuilder);

        return services.BuildServiceProvider();
    }

    private IConfiguration SetupConfiguration(Dictionary<string, string> options)
    {
        var configBuilder = new ConfigurationBuilder();

        configBuilder.AddInMemoryCollection(options);
        return configBuilder.Build();
    }
}
