using Dfe.Data.SearchPrototype.Search.Application.Adapters;
using DfE.Data.ComponentLibrary.CleanArchitecture.CleanArchitecture.Application.UseCase;

namespace Dfe.Data.SearchPrototype.Search.Application.UseCases.SearchForEstablishments
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SearchForEstablishmentsUseCase : IUseCase<SearchForEstablishmentsRequest, SearchForEstablishmentsResponse>
    {
        private readonly ISearchServiceAdapter _searchServiceAdapter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchServiceAdapter"></param>
        public SearchForEstablishmentsUseCase(ISearchServiceAdapter searchServiceAdapter)
        {
            _searchServiceAdapter = searchServiceAdapter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<SearchForEstablishmentsResponse> HandleRequest(SearchForEstablishmentsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
