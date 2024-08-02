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
    /// <summary>
    /// First line of the address
    /// </summary>
    public string? STREET { get; set; }
    /// <summary>
    /// Second line of the address of the retrieved establishment result
    /// </summary>
    public string? LOCALITY { get; set; }
    /// <summary>
    /// Third line of the address of the retrieved establishment result
    /// </summary>
    public string? ADDRESS3 { get; set; }
    /// <summary>
    /// Fourth line of the address of the retrieved establishment result
    /// </summary>
    public string? TOWN { get; set; }
    /// <summary>
    /// Postcode of the retrieved establishment result
    /// </summary>
    public string? POSTCODE { get; set; }
    /// <summary>
    /// The type of the establishment of the retrieved establishment result
    /// </summary>
    public string? TYPEOFESTABLISHMENTNAME { get; set; }
    /// <summary>
    /// "1" if the establishment includes the Primary phase of education
    /// "0" if the establishment does not include the Primary phase of education
    /// </summary>
    public string? ISPRIMARY { get; set; }
    /// <summary>
    /// "1" if the establishment includes the secondary phase of education
    /// "0" if the establishment does not include the secondary phase of education
    /// </summary>
    public string? ISSECONDARY { get; set; }
    /// <summary>
    /// "1" if the establishment includes the post 16 phase of education
    /// "0" if the establishment does not include the post 16 phase of education
    /// </summary>
    public string? ISPOST16 { get; set; }
    /// <summary>
    /// The status of the establishment of the retrieved establishment result
    /// If "1" establishment status is open if "0" it's closed
    /// </summary>
    public string? ESTABLISHMENTSTATUSCODE { get; set; }
}
