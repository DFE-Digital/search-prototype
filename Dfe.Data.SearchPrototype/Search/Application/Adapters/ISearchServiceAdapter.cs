using Dfe.Data.SearchPrototype.Search.Domain.AggregateRoot;

namespace Dfe.Data.SearchPrototype.Search.Application.Adapters
{
    /// <summary>
    /// Describes behaviour for an adaption of core search services infrastructure to allow
    /// compatibility with the Dfe.Data.SearchPrototype application search service definition.
    /// </summary>
    public interface ISearchServiceAdapter
    {
        /// <summary>
        /// Describes the required call to the underlying search service infrastructure and the expected
        /// "T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments" type to be returned.
        /// </summary>
        /// <param name="searchContext">
        /// Prescribes the context of the search including the keyword and collection target.
        /// </param>
        /// <returns>
        /// A configured "T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments"
        /// object hydrated from the results of the azure search.
        /// </returns>
        Task<Establishments> Search(SearchContext searchContext);
    }
}