using Azure.Search.Documents;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options.Mapping;

/// <summary>
/// 
/// </summary>
public sealed class SearchOptionsToAzureOptionsMapper : IMapper<SearchSettingsOptions, SearchOptions>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public SearchOptions MapFrom(SearchSettingsOptions input)
    {
        var searchOptions = new SearchOptions()
        {
            SearchMode = input.SearchMode,
            Size = input.Size,
            IncludeTotalCount = input.IncludeTotalCount
        };

        input.SearchFields?.ToList()
            .ForEach(searchfield =>
                searchOptions.SearchFields.Add(searchfield));

        return searchOptions;
    }

}
