namespace Dfe.Data.SearchPrototype.Web.Models;

/// <summary>
/// A view model representation of a single search result.
/// </summary>
public class SearchItemViewModel
{
    /// <summary>
    /// Establishment Urn.
    /// </summary>
    public string Urn { get; set; } = string.Empty;
    /// <summary>
    /// Establishment name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}