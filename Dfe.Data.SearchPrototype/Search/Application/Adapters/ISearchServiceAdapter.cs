using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot;

namespace Dfe.Data.SearchPrototype.Search.Application.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISearchServiceAdapter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchContext"></param>
        /// <returns></returns>
        Task<Establishments> Search(SearchContext searchContext);
    }
}