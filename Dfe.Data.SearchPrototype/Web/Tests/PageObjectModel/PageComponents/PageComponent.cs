using AngleSharp.Dom;

namespace Dfe.Data.SearchPrototype.Web.Tests.PageObjectModel.PageComponents
{
    public abstract class PageComponent
    {
        protected IElement? PageElement { get; private set; }

        protected PageComponent(IDocument documentObjectModel, string tagName)
        {
            SetPageElement(documentObjectModel, tagName);
        }

        private void SetPageElement(
            IDocument documentObjectModel, string tagName)
        {
            PageElement =
                documentObjectModel.GetElementsByTagName(tagName).SingleOrDefault() ??
                throw new InvalidOperationException(
                    $"Unable to derive element {tagName}.");
        }
    }
}
