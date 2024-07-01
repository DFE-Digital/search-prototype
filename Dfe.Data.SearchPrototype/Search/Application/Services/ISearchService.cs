using Dfe.Data.SearchPrototype.Search.Domain;

namespace Dfe.Data.SearchPrototype.Search.Application.Services
{
    public interface ISearchService
    {
        Task<SearchResults> SearchByKeyword(string keyword);
    }
}