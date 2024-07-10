namespace Dfe.Data.SearchPrototype.Search
{
    public interface ISearchService
    {
        Task<SearchResults> SearchByKeyword(string keyword);
    }
}