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

    public AddressViewModel Address { get; init; } = new();

    public EducationPhaseViewModel EducationPhase { get; init; } = new();

    public string EducationPhaseAsString()
    {
        var mapEducationPhaseCodeToString = new Dictionary<string, string>
        {
            {"PRIMARY", EducationPhase.IsPrimary },
            {"SECONDARY", EducationPhase.IsSecondary },
            {"16 PLUS", EducationPhase.IsPost16 }
        };
        var educationPhaseComponents = mapEducationPhaseCodeToString
            .Where(educationPhaseCode => educationPhaseCode.Value == "1")
            .Select(educationPhaseCode => educationPhaseCode.Key);

        return string.Join(", ", educationPhaseComponents);
    }
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