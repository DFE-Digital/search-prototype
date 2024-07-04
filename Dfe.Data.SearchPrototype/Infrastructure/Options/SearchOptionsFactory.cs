using Azure.Search.Documents;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Microsoft.Extensions.Options;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options;

/// <summary>
/// 
/// </summary>
public class SearchOptionsFactory : ISearchOptionsFactory
{
    private readonly IOptionsSnapshot<SearchSettingsOptions> _searchSettingsOptions;
    private readonly IMapper<SearchSettingsOptions, SearchOptions> _searchOptionsToAzureOptionsMapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="searchSettingsOptions"></param>
    /// <param name="searchOptionsToAzureOptionsMapper"></param>
    public SearchOptionsFactory(
        IOptionsSnapshot<SearchSettingsOptions> searchSettingsOptions,
        IMapper<SearchSettingsOptions, SearchOptions> searchOptionsToAzureOptionsMapper)
    {
        _searchSettingsOptions = searchSettingsOptions;
        _searchOptionsToAzureOptionsMapper = searchOptionsToAzureOptionsMapper;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetCollection"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
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
