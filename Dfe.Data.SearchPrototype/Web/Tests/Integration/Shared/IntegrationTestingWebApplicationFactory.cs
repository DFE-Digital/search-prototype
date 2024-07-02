using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Test.Shared;

public class IntegrationTestingWebApplicationFactory : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _fixture;
    protected HttpClient _httpClient;

    public IntegrationTestingWebApplicationFactory(WebApplicationFactory<Program> fixture)
    {
        _fixture = fixture;
        _httpClient = _fixture.CreateClient();
    }
}
