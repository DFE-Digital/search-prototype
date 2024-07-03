using Dfe.Data.SearchPrototype.Infrastructure.Options.Context;
using Microsoft.Extensions.Options;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options;

public class SearchSettingsOptionsFactory : ISearchSettingsOptionsFactory
{
    private readonly IOptionsSnapshot<SearchSettingsOptions> _searchSettingsOptions;

    public SearchSettingsOptionsFactory(IOptionsSnapshot<SearchSettingsOptions> searchSettingsOptions)
    {
        _searchSettingsOptions = searchSettingsOptions;
    }

    public SearchSettingsOptions GetSearchOptions(SearchCollectionContext searchCollectionContext)
    {
        return _searchSettingsOptions.Get(searchCollectionContext.ToString());
    }
}
