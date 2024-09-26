using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Filtering;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.Integration.TestHarness;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.Shared;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Integration;

public class CognitiveSearchServiceAdaptorIntegrationTests :IClassFixture<ConfigBuilder>, IClassFixture<MyServiceProvider>
{
    private AzureSearchOptions _options = AzureSearchOptionsTestDouble.Stub();
    private IConfiguration _configuration;
    private IServiceProvider _serviceProvider;

    private static Dictionary<string, string> InMemoryConfig => new Dictionary<string, string> {
            //{"FilterKeyToFilterExpressionMapOptions:FilterChainingLogicalOperator", "AndLogicalOperator"},
            //{"FilterKeyToFilterExpressionMapOptions:SearchFilterToExpressionMap:PHASEOFEDUCATION", "SearchInFilterExpression" },
            //{"FilterKeyToFilterExpressionMapOptions:SearchFilterToExpressionMap:ESTABLISHMENTSTATUSNAME", "SearchInFilterExpression" }
        };

    public CognitiveSearchServiceAdaptorIntegrationTests(ConfigBuilder configBuilder, MyServiceProvider serviceProvider)
    {
        _configuration = configBuilder.SetupConfiguration(InMemoryConfig);
        _serviceProvider = serviceProvider.SetUpServiceProvider(_configuration);
    }

    [Fact]
    public async Task  CompositionRoot_DoesEverythingItNeeds()
    {
        // Create system under test
        var adapter = new CognitiveSearchServiceAdapter<DataTransferObjects.Establishment>(
            _serviceProvider.GetRequiredService<ISearchByKeywordService>(),
            IOptionsTestDouble.IOptionsMockFor(_options),
            _serviceProvider.GetRequiredService<IMapper<Pageable<SearchResult<DataTransferObjects.Establishment>>, EstablishmentResults>>(),
            _serviceProvider.GetRequiredService<IMapper<Dictionary<string, IList<Azure.Search.Documents.Models.FacetResult>>, EstablishmentFacets>>(),
            _serviceProvider.GetRequiredService<ISearchFilterExpressionsBuilder>()
            );

        var searchServiceAdapterRequest = SearchServiceAdapterRequestTestDouble.Create();

        // act
        var response = await adapter.SearchAsync(searchServiceAdapterRequest);
    }
}
