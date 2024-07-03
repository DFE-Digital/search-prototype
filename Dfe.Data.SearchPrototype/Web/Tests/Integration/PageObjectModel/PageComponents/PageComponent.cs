using AngleSharp.Dom;

namespace Dfe.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents
{
    public abstract class PageComponent
    {
        protected IElement? HeaderElement { get; private set; }

        protected PageComponent(IDocument documentObjectModel, string tagName)
        {
            SetPageElement(documentObjectModel, tagName);
        }

        private void SetPageElement(
            IDocument documentObjectModel, string tagName)
        {
            HeaderElement =
                documentObjectModel.GetElementsByTagName(tagName).SingleOrDefault() ??
                throw new InvalidOperationException(
                    $"Unable to derive element {tagName}.");
        }
    }
}
