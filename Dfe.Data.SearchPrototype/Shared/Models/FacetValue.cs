namespace Dfe.Data.SearchPrototype.Shared.Models;

/// <summary>
/// Encapsulates a single facet result and count for a given fact type.
/// </summary>
public class FacetValue
{
    /// <summary>
    /// The value of the facet result.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// The number of records that belong to this facet value.
    /// </summary>
    public long? Count { get; }


    /// <summary>
    ///  Establishes an immutable <see cref="FacetValue"/> instance via the constructor arguments specified.
    /// </summary>
    /// <param name="value">
    /// The values associated with the given facet type.
    /// </param>
    /// <param name="count">
    /// The number of records that belong to this facet value.
    /// </param>
    public FacetValue(string value, long? count)
    {
        Value = value;
        Count = count;
    }
}
