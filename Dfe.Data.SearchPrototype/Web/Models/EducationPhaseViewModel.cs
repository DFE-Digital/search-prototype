namespace Dfe.Data.SearchPrototype.Web.Models;

/// <summary>
/// A view model representation of an education phase from a single search result.
/// </summary>
public class EducationPhaseViewModel
{
    /// <summary>
    /// true if the establishment includes the Primary phase of education
    /// false if the establishment does not include the Primary phase of education
    /// </summary>
    public bool IsPrimary { get; set; }
    /// <summary>
    /// true if the establishment includes the secondary phase of education
    /// false if the establishment does not include the secondary phase of education 
    /// </summary>
    public bool IsSecondary { get; set; }
    /// <summary>
    /// true if the establishment includes the post 16 phase of education
    /// false if the establishment does not include the post 16 phase of education 
    /// </summary>
    public bool IsPost16 { get; set; }
}