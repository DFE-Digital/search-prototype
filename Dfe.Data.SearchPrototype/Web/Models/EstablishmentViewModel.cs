using DfE.Data.ComponentLibrary.Infrastructure.CognitiveSearch.Search.Model;

namespace Dfe.Data.SearchPrototype.Web.Models;

/// <summary>
/// A view model representation of a single search result.
/// </summary>
public class EstablishmentViewModel
{
    /// <summary>
    /// Establishment Urn.
    /// </summary>
    public string Urn { get; set; } = string.Empty;
    /// <summary>
    /// Establishment name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    public AddressViewModel Address { get; set; } = new();

    public string AddressAsString()
    {
        var addressComponents
            =
            new[] { Address.Street, Address.Locality, Address.Address3, Address.Town, Address.Postcode }
                .Where(addressComponent => !string.IsNullOrEmpty(addressComponent))
                .ToArray();
        var readableAddress = string.Join(", ", addressComponents);

        return readableAddress;
    }
}