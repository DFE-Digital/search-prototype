using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Mapping.Extensions;
using Dfe.Data.SearchPrototype.Search.Domain.AggregateRoot.ValueObjects;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using System.Dynamic;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mapping
{
    /// <summary>
    /// Facilitates mapping from the received T:Azure.Search.Documents.Models.SearchResults
    /// into the required T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects.EstablishmentIdentifier object.
    /// </summary>
    public sealed class EstablishmentIdentityMapper : IMapper<SearchResult<object>, EstablishmentIdentifier>
    {
        private const string MapKey = "SearchResultToEstablishmentIdentityMap";

        private readonly IObjectFactoryMapper _objectFactoryMapper;

        /// <summary>
        /// Mapper is injected with an T:DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping.IObjectFactoryMapper
        /// instance and uses the configuration map key 'SearchResultToEstablishmentIdentityMap' to target the
        /// configuration options for this particular mapping definition, the complete implementation of which is defined in app-settings.
        /// </summary>
        /// <param name="objectFactoryMapper">
        /// The T:DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping.IObjectFactoryMapper definition injected via IOC container.
        /// </param>
        public EstablishmentIdentityMapper(IObjectFactoryMapper objectFactoryMapper)
        {
            _objectFactoryMapper = objectFactoryMapper;
        }

        /// <summary>
        /// Object factory mapper definition for automatically wiring the mapping fields (described by app settings).
        /// </summary>
        /// <param name="input">
        /// The T:Azure.Search.Documents.Models.SearchResult instance to map from.
        /// </param>
        /// <returns>
        /// The target T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects.EstablishmentIdentifier to be mapped and returned.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// The exception thrown if a document cannot be derived from a given Azure search result.
        /// </exception>
        public EstablishmentIdentifier MapFrom(SearchResult<object> input)
        {
            ExpandoObject? searchResult = input.DeserialiseSearchResultDocument();

            return searchResult == null ?
                throw new ArgumentException(
                    $"Unable to derive search result for establishment identity map with input: {input} ") :
                _objectFactoryMapper.Map<dynamic, EstablishmentIdentifier>(searchResult!, MapKey);
        }
    }
}
