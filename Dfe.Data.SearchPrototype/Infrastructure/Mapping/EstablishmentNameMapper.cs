using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Mapping.Extensions;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using System.Dynamic;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EstablishmentNameMapper : IMapper<SearchResult<object>, EstablishmentName>
    {
        private const string MapKey = "SearchResultToEstablishmentNameMap";

        private readonly IObjectFactoryMapper _objectFactoryMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectFactoryMapper">
        /// 
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 
        /// </exception>
        public EstablishmentNameMapper(IObjectFactoryMapper objectFactoryMapper)
        {
            _objectFactoryMapper = objectFactoryMapper ??
                throw new ArgumentNullException(nameof(objectFactoryMapper));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// 
        /// </exception>
        public EstablishmentName MapFrom(SearchResult<object> input)
        {
            ExpandoObject? searchResult = input.DeserialiseSearchResultDocument();

            return searchResult == null ?
                throw new ArgumentException(
                    $"Unable to derive search result for establishment name map with input: {input}.") :
                _objectFactoryMapper.Map<dynamic, EstablishmentName>(searchResult!, MapKey);
        }
    }
}
