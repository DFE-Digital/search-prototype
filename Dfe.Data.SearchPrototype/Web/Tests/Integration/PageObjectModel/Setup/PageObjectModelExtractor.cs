using AngleSharp;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;

public class PageObjectModelExtractor : WebApplicationBootstrapper
{
    public PageObjectModelExtractor(WebApplicationFactory<Program> webApplicationFactory)
        : base(webApplicationFactory){
    }

    protected async Task<IElement?> GetPageElement(string pageName, string tagName)
    {
        IDocument? response = await GetPageObject(pageName) ??
            throw new InvalidOperationException(
                $"Unable to derive web page {pageName}.");

        IElement? pageElement =
            response.GetElementsByTagName(tagName).SingleOrDefault();

        return pageElement ??
            throw new InvalidOperationException(
                $"Unable to derive element for page {pageName}.");
    }

    protected async Task<IDocument> GetPageObject(string pageName)
    {
        HttpResponseMessage response = await HttpClient.GetAsync(pageName);
        string documentObjectModel = await response.Content.ReadAsStringAsync();

        return await BrowsingContext
            .New(Configuration.Default)
            .OpenAsync(response => response.Content(documentObjectModel));
    }
}