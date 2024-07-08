using Azure.Search.Documents;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options;

/// <summary>
/// 
/// </summary>
public interface ISearchOptionsFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetCollection"></param>
    /// <returns></returns>
    public SearchOptions? GetSearchOptions(string targetCollection);
}