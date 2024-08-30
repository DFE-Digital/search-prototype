namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// The object that encapsulates a single facet result for a facet 
/// </summary>
public class FacetResult
{
    /// <summary>
    /// The value of the facet result
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// The number of records that belong to this facet value
    /// </summary>
    public long? Count { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="value">The value of the facet result</param>
    /// <param name="count">The number of records that belong to this facet value</param>
    public FacetResult(string value, int? count)
    {
        Value = value;
        Count = count;
    }
}
