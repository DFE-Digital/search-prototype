namespace Dfe.Data.SearchPrototype.Infrastructure;

/// <summary>
/// Object used to encapsulate the core response from an Azure cognitive search result.
/// </summary>
public class Establishment
{
    /// <summary>
    /// The unique identifier of the retrieved establishment result.
    /// </summary>
    public string? id { get; set; }
    /// <summary>
    /// The name associated with the retrieved establishment result.
    /// </summary>
    public string? ESTABLISHMENTNAME { get; set; }
}
