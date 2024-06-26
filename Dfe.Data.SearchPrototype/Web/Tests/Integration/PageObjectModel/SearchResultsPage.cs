using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel
{
    public sealed class SearchResultsPage : PageObjectExtractor
    {
        public SearchResultsPage(WebApplicationFactory<Program> webApplicationFactory) :
            base(webApplicationFactory)
        {
            PageHeader = PageHeader.Create(webApplicationFactory);
        }

        public PageHeader PageHeader { get; }

        public static SearchResultsPage Create(
            WebApplicationFactory<Program> webApplicationFactory) => new(webApplicationFactory);
    }
}
