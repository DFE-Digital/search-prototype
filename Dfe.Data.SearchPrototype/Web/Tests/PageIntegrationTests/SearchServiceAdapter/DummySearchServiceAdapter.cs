using Dfe.Data.SearchPrototype.Search;
using Dfe.Data.SearchPrototype.Web.Tests.PageIntegrationTests.SearchServiceAdapter.Resources;
using Newtonsoft.Json.Linq;

namespace Dfe.Data.SearchPrototype.Web.Tests.PageIntegrationTests.SearchServiceAdapter
{
    public sealed class DummySearchServiceAdapter<TSearchResult> : ISearchServiceAdapter where TSearchResult : class
    {
        private readonly IJsonFileLoader _jsonFileLoader;

        public DummySearchServiceAdapter(IJsonFileLoader jsonFileLoader)
        {
            _jsonFileLoader = jsonFileLoader;
        }

        public async Task<EstablishmentResults> SearchAsync(SearchContext searchContext)
        {
            string json = await _jsonFileLoader.LoadJsonFile();

            JObject establishmentsObject = JObject.Parse(json);

            IEnumerable<Establishment> establishments =
                from establishmentToken in establishmentsObject["establishments"]
                where establishmentToken["name"]!.ToString().Contains(searchContext.SearchKeyword)
                select new Establishment(
                    (string)establishmentToken["urn"]!, 
                    (string)establishmentToken["name"]!);

            EstablishmentResults results = new();
            
            establishments.ToList().ForEach(establishment => results.AddEstablishment(establishment));
            
            return results;

        }
    }
}
