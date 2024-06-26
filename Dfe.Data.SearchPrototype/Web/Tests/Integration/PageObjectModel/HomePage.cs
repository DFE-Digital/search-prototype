using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel
{
    public sealed class HomePage : PageObjectExtractor
    {
        public HomePage(WebApplicationFactory<Program> webApplicationFactory) :
            base(webApplicationFactory)
        {
            SearchHeader = SearchHeader.Create(webApplicationFactory);
        }

        public SearchHeader SearchHeader { get; }

        public static HomePage Create(
            WebApplicationFactory<Program> webApplicationFactory) => new(webApplicationFactory);
    }
}