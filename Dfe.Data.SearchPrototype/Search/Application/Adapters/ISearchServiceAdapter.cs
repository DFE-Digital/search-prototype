using Dfe.Data.SearchPrototype.Search.Domain;

namespace Dfe.Data.SearchPrototype.Search.Application.Adapters
{
    public interface ISearchServiceAdapter
    {
        Task<Establishments> Search(SearchContext searchContext);
    }
}