using AngleSharp;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;

public class PageObjectExtractor : WebApplicationBootstrapper
{
    public PageObjectExtractor(WebApplicationFactory<Program> webApplicationFactory)
        : base(webApplicationFactory){
    }

    protected async Task<IDocument> GetPageObject(string webPage)
    {
        HttpResponseMessage response = await HttpClient.GetAsync(webPage);
        string documentObjectModel = await response.Content.ReadAsStringAsync();

        return await BrowsingContext
            .New(Configuration.Default)
            .OpenAsync(response => response.Content(documentObjectModel));
    }
}
