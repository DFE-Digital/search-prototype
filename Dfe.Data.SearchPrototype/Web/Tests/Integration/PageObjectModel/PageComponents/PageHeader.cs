using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents
{
    public class PageHeader : PageObjectModelExtractor
    {
        public PageHeader(WebApplicationFactory<Program> webApplicationFactory) :
            base(webApplicationFactory){
        }

        public async Task<string> GetHeading()
        {
            IElement? searchHeading = await GetSearchHeaderElement("h1");

            return searchHeading == null ?
                throw new InvalidOperationException("Unable to derive the search heading.") :
                searchHeading.InnerHtml;
        }

        public async Task<IHtmlAnchorElement> GetSearchHeaderLink(string linkName)
        {
            IElement? searchHeader = await GetSearchHeaderElement("header");

            return searchHeader == null ?
                throw new InvalidOperationException($"Unable to derive the search link: {linkName} in page.") :
                (IHtmlAnchorElement)searchHeader
                    .GetElementsByTagName("a")
                    .Single(anchorTags => anchorTags.TextContent.Contains(linkName));
        }

        private async Task<IElement?> GetSearchHeaderElement(string tagName)
        {
            IDocument? response = await GetPageObject("");
            return response.GetElementsByTagName(tagName).SingleOrDefault();
        }

        public static PageHeader Create(
            WebApplicationFactory<Program> webApplicationFactory) => new(webApplicationFactory);
    }
}