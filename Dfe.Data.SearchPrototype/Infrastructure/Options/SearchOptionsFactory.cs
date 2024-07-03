using Azure.Search.Documents;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Microsoft.Extensions.Options;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options;

public class SearchOptionsFactory : ISearchOptionsFactory
{
    private readonly IOptionsSnapshot<SearchSettingsOptions> _searchSettingsOptions;
    private readonly IMapper<SearchSettingsOptions, SearchOptions> _searchOptionsToAzureOptionsMapper;

    public SearchOptionsFactory(
        IOptionsSnapshot<SearchSettingsOptions> searchSettingsOptions,
        IMapper<SearchSettingsOptions, SearchOptions> searchOptionsToAzureOptionsMapper)
    {
        _searchSettingsOptions = searchSettingsOptions;
        _searchOptionsToAzureOptionsMapper = searchOptionsToAzureOptionsMapper;
    }

    public SearchOptions GetSearchOptions(string targetCollection)
    {
        if (string.IsNullOrWhiteSpace(targetCollection))
        {
            throw new ArgumentNullException(nameof(targetCollection));
        }
        SearchSettingsOptions searchSettingsOptions =
            _searchSettingsOptions.Get(targetCollection);

        return _searchOptionsToAzureOptionsMapper.MapFrom(searchSettingsOptions);
    }
}
