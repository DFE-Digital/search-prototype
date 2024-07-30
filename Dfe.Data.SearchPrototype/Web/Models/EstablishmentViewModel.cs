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
    /// Establishment type.
    /// </summary>
    public string EstablishmentType {  get; init; } = string.Empty;

    public EducationPhaseViewModel EducationPhase { get; init; } = new();

    public string EducationPhaseAsString()
    {
        var mapEducationPhaseCodeToString = new Dictionary<string, bool>
        {
            {"Primary", EducationPhase.IsPrimary },
            {"Secondary", EducationPhase.IsSecondary },
            {"16 plus", EducationPhase.IsPost16 }
        };
        var educationPhaseComponents = mapEducationPhaseCodeToString
            .Where(educationPhaseCode => educationPhaseCode.Value == true)
            .Select(educationPhaseCode => educationPhaseCode.Key);

        return string.Join(", ", educationPhaseComponents);
    }
    /// <summary>
    /// Establishment address.
    /// </summary>
    /// <returns>
    /// Address formatted as a display-friendly string
    /// </returns>
    public string AddressAsString()
    {
        var addressComponents
            = new[] { Address.Street, Address.Locality, Address.Address3, Address.Town, Address.Postcode }
                .Where(addressComponent => !string.IsNullOrEmpty(addressComponent))
                .ToArray();
        var readableAddress = string.Join(", ", addressComponents);

        return readableAddress;
    }
}