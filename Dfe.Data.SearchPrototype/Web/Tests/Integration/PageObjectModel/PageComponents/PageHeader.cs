using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Dfe.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents
{
    public sealed class PageHeader : PageComponent
    {
        private const string HeaderElementTag = "header";

        private PageHeader(IDocument documentObjectModel)
            : base(documentObjectModel, HeaderElementTag){
        }

        public string GetMainHeading(string headingClass) =>
            PageElement == null ?
                throw new InvalidOperationException(
                    "Unable to derive the main heading in page.") :
                PageElement
                    .GetElementsByClassName(headingClass).Single().InnerHtml;

        public IHtmlAnchorElement GetHeaderLink(string linkName) =>
            PageElement == null ?
                throw new InvalidOperationException(
                    $"Unable to derive the search link: {linkName} in page.") :
                (IHtmlAnchorElement)PageElement
                    .GetElementsByTagName("a")
                    .Single(anchorTags => anchorTags.TextContent.Contains(linkName));

        public static PageHeader Create(IDocument documentObjectModel) => new(documentObjectModel);
    }
}