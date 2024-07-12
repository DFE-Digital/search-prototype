using AngleSharp;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;

public abstract class DocumentObjectModelExtractor : WebApplicationBootstrapper
{
    protected IDocument? DocumentObjectModel { get; private set; }

    protected DocumentObjectModelExtractor(
        WebApplicationFactory<Program> webApplicationFactory, string? pageName)
        : base(webApplicationFactory)
    {
        if (string.IsNullOrWhiteSpace(pageName)){
            throw new ArgumentNullException(nameof(pageName));
        }

        SetPageObject(pageName);
    }

    protected void SetPageObject(string pageName)
    {
        Task.Run(async () => {
            HttpResponseMessage response = await HttpClient.GetAsync(pageName);
            string documentObjectModel = await response.Content.ReadAsStringAsync();

            DocumentObjectModel = await BrowsingContext
                .New(Configuration.Default)
                .OpenAsync(response => response.Content(documentObjectModel));
        })
        .Wait();

        if (DocumentObjectModel == null){
            throw new InvalidOperationException(
                $"Unable to derive document object model for page {pageName}.");
        }
    }
}