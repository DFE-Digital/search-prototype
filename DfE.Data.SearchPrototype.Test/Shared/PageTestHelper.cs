using AngleSharp;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Test.Shared;

public class PageTestHelper : IntegrationTestingWebApplicationFactory
{
    private IBrowsingContext _context;

    public PageTestHelper(WebApplicationFactory<Program> factory) : base(factory)
    {
        // anglesharp
        AngleSharp.IConfiguration angleSharpConfig = Configuration.Default;
        _context = BrowsingContext.New(angleSharpConfig);
        //--------------------------------------------------------------------
    }

    protected async Task<IDocument> NavigateToPageAsync(string webPage)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(webPage);

        var DOM = await response.Content.ReadAsStringAsync();

        return await _context.OpenAsync(req => req.Content(DOM));
    }
}
