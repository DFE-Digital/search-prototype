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

    public string? STREET { get; set; }

    public string? LOCALITY { get; set; }

    public string? ADDRESS3 { get; set; }

    public string? TOWN { get; set; }

    public string? POSTCODE { get; set; }
}
