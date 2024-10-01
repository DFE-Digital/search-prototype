using Dfe.Data.SearchPrototype.Common.CleanArchitecture.Application.UseCase;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;
using Dfe.Data.SearchPrototype.Tests.Integration.TestHarness;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Dfe.Data.SearchPrototype.Tests.Integration;

public class CompositionRootTests : IClassFixture<CompositionRootServiceProvider>, IClassFixture<ConfigBuilder>
{
    private Dictionary<string, string?> InMemoryConfig => new()  {
        { "SearchByKeywordCriteria:SearchFields:0", "ESTABLISHMENTNAME" },
        { "SearchByKeywordCriteria:Facets:0", "FACET1"}
    };
    private readonly IConfiguration _config;
    private readonly IServiceProvider _compositionRootServiceProvider;

    public CompositionRootTests(CompositionRootServiceProvider serviceProvider, ConfigBuilder configBuilder)
    {
        _config = configBuilder.SetupConfiguration(InMemoryConfig);
        _compositionRootServiceProvider = serviceProvider.SetUpServiceProvider(_config);
    }

    [Fact]
    public async Task AddSearchForEstablishmentServices_CanCreateUsecase()
    {
        var usecase = _compositionRootServiceProvider.GetRequiredService<IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>>();

        var response = await usecase.HandleRequest(new SearchByKeywordRequest("searchkeyword"));

        response.Should().NotBeNull();
    }
}
