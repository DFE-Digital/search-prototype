using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Dfe.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents
{
    public sealed class SearchComponent : PageComponent
    {
        private const string SearchElementTag = "form";

        private SearchComponent(IDocument documentObjectModel)
            : base(documentObjectModel, SearchElementTag){
        }

        public static SearchComponent Create(IDocument documentObjectModel) => new(documentObjectModel);

        public IHtmlInputElement GetTextInputBox(string tagName) =>
            PageElement == null ?
                throw new InvalidOperationException(
                    "Unable to derive the search input box.") :
                (IHtmlInputElement)PageElement
                    .GetElementsByTagName(tagName).Single(); // TODO try to get by the name

        public async Task ClickSubmitAsync()
        {
            var whatOnEarth = (IHtmlFormElement)PageElement;
            whatOnEarth.SubmitAsync();


            //Task.Run(() => {
            //    ((IHtmlFormElement)PageElement!).SubmitAsync();
            //}).Wait();
        }

        public IHtmlButtonElement GetSubmitButton(string tagName) =>
             PageElement == null ?
                throw new InvalidOperationException(
                    "Unable to derive the search submit button.") :
                (IHtmlButtonElement)PageElement
                    .GetElementsByTagName(tagName).Single();
    }
}