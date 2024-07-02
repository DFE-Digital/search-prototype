using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents
{
    public sealed class PageHeader : PageObjectModelExtractor
    {
        private IElement? HeaderElement { get; set; }

        private PageHeader(
            WebApplicationFactory<Program> webApplicationFactory) :
            base(webApplicationFactory){
        }

        public string GetMainHeading(string headingClass) =>
            HeaderElement == null ?
                throw new InvalidOperationException(
                    "Unable to derive the main heading in page.") :
                HeaderElement
                    .GetElementsByClassName(headingClass).Single().InnerHtml;

        public IHtmlAnchorElement GetHeaderLink(string linkName) =>
            HeaderElement == null ?
                throw new InvalidOperationException(
                    $"Unable to derive the search link: {linkName} in page.") :
                (IHtmlAnchorElement)HeaderElement
                    .GetElementsByTagName("a")
                    .Single(anchorTags => anchorTags.TextContent.Contains(linkName));

        public static PageHeader Create(
            WebApplicationFactory<Program> webApplicationFactory, string pageName)
        {
            PageHeader pageHeader = new(webApplicationFactory);

            Task.Run(() =>
                pageHeader.HeaderElement =
                    pageHeader.GetHeader(pageName).Result ??
                    throw new InvalidOperationException(
                        $"Unable to derive header for page {pageName}.")
                )
                .Wait();

            return pageHeader;
        }

        private const string HeaderElementName = "header";

        private Task<IElement?> GetHeader(string pageName) => GetPageElement(pageName, HeaderElementName);
    }
}