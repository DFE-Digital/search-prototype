using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mapping
{
    /// <summary>
    /// Facilitates mapping from the received T:Azure.Search.Documents.Models.SearchResults
    /// into the required T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments object.
    /// </summary>
    public sealed class AzureSearchResponseToSearchResultsMapper : IMapper<Response<SearchResults<object>>, Establishments>
    {
        private readonly IMapper<SearchResult<object>, EstablishmentIdentifier> _establishmentIdentityMapper;
        private readonly IMapper<SearchResult<object>, EstablishmentDefinition> _establishmentNameMapper;

        /// <summary>
        /// The following dependencies provide the sub-mapping behaviour for creating a configured,
        /// T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments instance from the
        /// provided T:Azure.Search.Documents.Models.SearchResults, the complete implementation of
        /// which is defined in the IOC container.
        /// </summary>
        /// <param name="establishmentIdentityMapper">
        /// Mapper for handling hydration of the
        /// T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities.EstablishmentIdentifier
        /// object injected via IOC container.
        /// </param>
        /// <param name="establishmentNameMapper">
        /// Mapper for handling hydration of the
        /// T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities.EstablishmentName
        /// object injected via IOC container.
        /// </param>
        public AzureSearchResponseToSearchResultsMapper(
            IMapper<SearchResult<object>, EstablishmentIdentifier> establishmentIdentityMapper,
            IMapper<SearchResult<object>, EstablishmentDefinition> establishmentNameMapper)
        {
            _establishmentIdentityMapper = establishmentIdentityMapper;
            _establishmentNameMapper = establishmentNameMapper;
        }

        /// <summary>
        /// The mapping input is the raw Azure search response T:Azure.Search.Documents.Models.SearchResults
        /// and if any results are contained within the response a new T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments
        /// instance is created, with the responsibility of hydrating this root object and children delegated to the sub-mappers.
        /// </summary>
        /// <param name="input">
        /// A configured T:Azure.Search.Documents.Models.SearchResults instance.
        /// </param>
        /// <returns>
        /// A configured T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments instance.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Exception thrown if an invalid document is derived from the Azure search result.
        /// </exception>
        public Establishments MapFrom(Response<SearchResults<object>> input)
        {
            ArgumentNullException.ThrowIfNull(input);

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