using Azure.Search.Documents.Models;
using Azure;
using Dfe.Data.SearchPrototype.Infrastructure.Mapping;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using DfE.Data.ComponentLibrary.Infrastructure.CognitiveSearch.Options;
using DfE.Data.ComponentLibrary.Infrastructure.CognitiveSearch.Providers;
using DfE.Data.ComponentLibrary.Infrastructure.CognitiveSearch.Search;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests;

/// <summary>
/// Temporary test class so that I can see how Cog search behaves with various settings including mis-configurations.
/// </summary>
public class TemporaryTestsWithActualCogSearch
{
    private readonly IConfiguration _configuration;
    private readonly IOptions<AzureSearchClientOptions> _options;

    public TemporaryTestsWithActualCogSearch()
    {
        // read user secrets
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<TemporaryTestsWithActualCogSearch>()
            .Build();

        var optionSecretsCredentials = _configuration["AzureSearchClientOptions:Credentials"];
        var optionSecretsEndpointUri = _configuration["AzureSearchClientOptions:EndpointUri"];
        var options = new Mock<IOptions<AzureSearchClientOptions>>();
        options.Setup(options => options.Value)
            .Returns(new AzureSearchClientOptions()
            {
                Credentials = optionSecretsCredentials,
                EndpointUri = optionSecretsEndpointUri
            });
        _options = options.Object;
    }

    private static CognitiveSearchServiceAdapter<Establishment> CreateServiceAdapterWith(
        ISearchService cognitiveSearchService,
        ISearchOptionsFactory searchOptionsFactory,
        IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> searchResponseMapper
       ) =>
           new(cognitiveSearchService, searchOptionsFactory, searchResponseMapper);

    [Fact]
    public async Task Temp_call_Actual_CogSearch()
    {
        // arrange
        var searchOptions = SearchOptionsFactoryTestDouble.MockFor(new Azure.Search.Documents.SearchOptions
        {
            SearchMode = SearchMode.Any,
            Size = 100,
            IncludeTotalCount = true,
            SearchFields = { "ESTABLISHMENTNAME" },
            Select = { "ESTABLISHMENTNAME", "id" }
        });

        var indexNamesProvider = new SearchIndexNamesProvider(_options);

        IMapper<Establishment, Search.Establishment> azureSearchResultToEstablishmentMapper =
            new AzureSearchResultToEstablishmentMapper();

        ISearchServiceAdapter cognitiveSearchServiceAdapter =
            CreateServiceAdapterWith(
                new DefaultSearchService(
                    new AzureSearchClientProvider(
                        _options,
                        indexNamesProvider
                        )),
                searchOptions,
                new AzureSearchResponseToEstablishmentsMapper(azureSearchResultToEstablishmentMapper));

        // act.
        var results = await cognitiveSearchServiceAdapter.Search(
                    new SearchContext(
                        searchKeyword: "Durham",
                        targetCollection: "establishments"));

    }
}
