using Dfe.Data.SearchPrototype.Search.Application.Adapters;
using DfE.Data.ComponentLibrary.CleanArchitecture.CleanArchitecture.Application.UseCase;

namespace Dfe.Data.SearchPrototype.Search.Application.UseCases.SearchForEstablishment
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SearchForEstablishmentUseCase : IUseCase<SearchForEstablishmentRequest, SearchForEstablishmentResponse>
    {
        private readonly ISearchServiceAdapter _searchServiceAdapter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchServiceAdapter"></param>
        public SearchForEstablishmentUseCase(ISearchServiceAdapter searchServiceAdapter)
        {
            _searchServiceAdapter = searchServiceAdapter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<SearchForEstablishmentResponse> HandleRequest(SearchForEstablishmentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
