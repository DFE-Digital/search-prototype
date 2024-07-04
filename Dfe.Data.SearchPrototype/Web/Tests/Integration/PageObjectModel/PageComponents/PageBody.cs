using AngleSharp.Dom;
using Dfe.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents
{
    public sealed class PageBody : PageComponent
    {
        private const string BodyElementTag = "body";

        private PageBody(IDocument documentObjectModel) : base(documentObjectModel, BodyElementTag){
        }

        public static PageBody Create(IDocument documentObjectModel) => new(documentObjectModel);
    }
}