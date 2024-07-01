using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;

public class WebApplicationBootstrapper : IClassFixture<WebApplicationFactory<Program>>
{
    protected HttpClient HttpClient { get; }

    /// <summary>
    /// WebApplicationFactory<Program> is injected by xUnit, which creates and calls Dispose.
    /// </summary>
    public WebApplicationBootstrapper(WebApplicationFactory<Program> fixture)
    {
        HttpClient = fixture.CreateClient();
    }
}