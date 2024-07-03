using Dfe.Data.SearchPrototype.Search.Application.Services;
using DfE.Data.ComponentLibrary.CleanArchitecture.CleanArchitecture.Application.UseCase;

namespace Dfe.Data.SearchPrototype.Search.Application.UseCases.SearchByKeyword
{
    public sealed class SearchByKeywordUseCase : IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>
    {
        private readonly ISearchServiceAdapter _searchService;

        public SearchByKeywordUseCase(ISearchServiceAdapter searchService)
        {
            _searchService = searchService;
        }

        public Task<SearchByKeywordResponse> HandleRequest(SearchByKeywordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
