//using AngleSharp.Dom;
//using AngleSharp.Html.Dom;
//using Dfe.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents;

//namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents
//{
//    public sealed class SearchComponent : PageComponent
//    {
//        private const string searchComponentContainer = "div";

//        private SearchComponent(IDocument documentObjectModel)
//            : base(documentObjectModel, searchComponentContainer, isTagName: false)
//        {
//        }

//        public string GetSearchComponentContainer(string containerId) =>
//            PageElement == null ?
//                throw new InvalidOperationException(
//                    "Unable to derive the main heading in page.") :
//            PageElement
//                   .GetElementsByTagName("div").GetElementById(containerId).InnerHtml;

//        public IHtmlAnchorElement GetHeaderLink(string linkName) =>
//            PageElement == null ?
//                throw new InvalidOperationException(
//                    $"Unable to derive the search link: {linkName} in page.") :
//                (IHtmlAnchorElement)PageElement
//                    .GetElementsByTagName("a")
//                    .Single(anchorTags => anchorTags.TextContent.Contains(linkName));

//        public static SearchComponent Create(IDocument documentObjectModel) => new(documentObjectModel);
//    }
//}