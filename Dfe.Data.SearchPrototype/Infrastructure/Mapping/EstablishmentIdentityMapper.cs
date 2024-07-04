using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Mapping.Extensions;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EstablishmentIdentityMapper : IMapper<SearchResult<object>, EstablishmentIdentifier>
    {
        private const string MapKey = "SearchResultToEstablishmentIdentityMap";

        private readonly IObjectFactoryMapper _objectFactoryMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectFactoryMapper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EstablishmentIdentityMapper(IObjectFactoryMapper objectFactoryMapper)
        {
            _objectFactoryMapper = objectFactoryMapper ??
                throw new ArgumentNullException(nameof(objectFactoryMapper));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public EstablishmentIdentifier MapFrom(SearchResult<object> input)
        {
            var searchResult = input.DeserialiseSearchResultDocument();

            return searchResult == null ?
                throw new ArgumentException(
                    $"Unable to derive search result for establishment identity map with input: {input} ") :
                _objectFactoryMapper.Map<dynamic, EstablishmentIdentifier>(searchResult!, MapKey);
        }
    }
}
