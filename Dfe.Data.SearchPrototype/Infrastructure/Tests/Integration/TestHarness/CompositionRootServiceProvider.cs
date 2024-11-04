using Dfe.Data.Common.Infrastructure.CognitiveSearch.Filtering;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Integration.TestHarness;

public class CompositionRootServiceProvider
{
    public IServiceProvider SetUpServiceProvider(IConfiguration config)
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(config);

        // this is the extension method to add all the dependencies
        services.AddCognitiveSearchAdaptorServices();

        // Replace Common.Infrastructure services with mocks
        services.RemoveAll<ISearchByKeywordService>();
        var mockSearchService = new SearchServiceMockBuilder()
            .WithSearchKeywordAndCollection("searchKeyword", "establishments")
            .WithSearchResults(new SearchResultFakeBuilder()
                .WithSearchResults()
                .Create())
            .Create();
        services.AddScoped(provider => mockSearchService);
        services.RemoveAll<ISearchFilterExpressionsBuilder>();
        var mockFilterExpressionBuilder = new FilterExpressionBuilderTestDouble().Create();
        services.AddScoped(provider => mockFilterExpressionBuilder);

        return services.BuildServiceProvider();
    }
}
