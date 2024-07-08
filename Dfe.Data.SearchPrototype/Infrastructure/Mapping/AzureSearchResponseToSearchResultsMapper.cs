using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AzureSearchResponseToSearchResultsMapper : IMapper<Response<SearchResults<object>>, Establishments>
    {
        private readonly IMapper<SearchResult<object>, EstablishmentIdentifier> _establishmentIdentityMapper;
        private readonly IMapper<SearchResult<object>, EstablishmentName> _establishmentNameMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="establishmentIdentityMapper">
        /// 
        /// </param>
        /// <param name="establishmentNameMapper">
        /// 
        /// </param>
        public AzureSearchResponseToSearchResultsMapper(
            IMapper<SearchResult<object>, EstablishmentIdentifier> establishmentIdentityMapper,
            IMapper<SearchResult<object>, EstablishmentName> establishmentNameMapper)
        {
            _establishmentIdentityMapper = establishmentIdentityMapper;
            _establishmentNameMapper = establishmentNameMapper;
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
        /// <exception cref="InvalidOperationException">
        /// 
        /// </exception>
        public Establishments MapFrom(Response<SearchResults<object>> input)
        {
            var establismentResults = Establishments.Create();
            var results = input.Value.GetResults();

            if (results.Any())
            {
                results.ToList().ForEach(searchResult =>
                {
                    if (searchResult.Document == null)
                    {
                        throw new InvalidOperationException(
                            "Search result document object cannot be null.");
                    }

                    establismentResults.AddEstablismentResult(
                        new Establishment(
                            _establishmentIdentityMapper.MapFrom(searchResult),
                            _establishmentNameMapper.MapFrom(searchResult))
                        );
                });
            }

            return establismentResults;
        }
    }
}