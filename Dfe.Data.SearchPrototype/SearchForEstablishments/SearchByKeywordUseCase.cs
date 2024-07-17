using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CleanArchitecture.CleanArchitecture.Application.UseCase;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

public sealed class SearchByKeywordUseCase : IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>
{
    private readonly ISearchServiceAdapter _searchServiceAdapter;
    private readonly IMapper<EstablishmentResults, SearchByKeywordResponse> _resultsToResponseMapper;

    public SearchByKeywordUseCase(
        ISearchServiceAdapter searchServiceAdapter,
        IMapper<EstablishmentResults, SearchByKeywordResponse> resultsToResponseMapper)
    {
        _searchServiceAdapter = searchServiceAdapter;
        _resultsToResponseMapper = resultsToResponseMapper;
    }

    public async Task<SearchByKeywordResponse> HandleRequest(SearchByKeywordRequest request)
    {
        EstablishmentResults establishmentResults = await _searchServiceAdapter.SearchAsync(request.Context);

        return _resultsToResponseMapper.MapFrom(establishmentResults);
    }
}
