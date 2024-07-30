namespace Dfe.Data.SearchPrototype.Web.Models;

/// <summary>
/// A view model representation of a single search result.
/// </summary>
public class EstablishmentViewModel
{
    /// <summary>
    /// Establishment Urn.
    /// </summary>
    public string Urn { get; init; } = string.Empty;
    /// <summary>
    /// Establishment name.
    /// </summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>
    /// Establishment address.
    /// </summary>
    public AddressViewModel Address { get; init; } = new();
    /// <summary>
    /// Establishment address.
    /// </summary>
    /// <returns>
    /// Address formatted as a display-friendly string
    /// </returns>
    public string AddressAsString => Address.AddressAsString();
    /// <summary>
    /// Establishment type.
    /// </summary>
    public string EstablishmentType {  get; init; } = string.Empty;
    /// <summary>
    /// Establishment education phase
    /// </summary>
    public EducationPhaseViewModel EducationPhase { get; init; } = new();
    /// <summary>
    /// Establishment education phase
    /// </summary>
    /// <returns>
    /// Education phase formatted as a display-friendly string
    /// </returns>
    public string EducationPhaseAsString => EducationPhase.EducationPhaseAsString();
   
}