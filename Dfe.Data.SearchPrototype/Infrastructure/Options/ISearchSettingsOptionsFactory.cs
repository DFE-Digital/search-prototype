using Dfe.Data.SearchPrototype.Infrastructure.Options.Context;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options;

public interface ISearchSettingsOptionsFactory
{
    public SearchSettingsOptions GetSearchOptions(SearchCollectionContext searchCollectionContext);
}