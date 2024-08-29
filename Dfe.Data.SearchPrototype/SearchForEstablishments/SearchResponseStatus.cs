namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// The status of the search response
/// </summary>
public enum SearchResponseStatus
{
    /// <summary>
    /// The search request completed successfully
    /// </summary>
    Success,
    /// <summary>
    /// The request was not valid
    /// </summary>
    InvalidRequest,
    /// <summary>
    /// The request was submitted and resulted in an error
    /// </summary>
    SearchServiceError
}