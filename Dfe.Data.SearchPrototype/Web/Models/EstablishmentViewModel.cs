﻿namespace Dfe.Data.SearchPrototype.Web.Models;

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
    /// <summary>
    /// Establishment status code.
    /// </summary>
    public string EstablishmentStatusCode { get; init; } = string.Empty;
    /// <summary>
    /// Establishment status displayed as user friendly string
    /// if "1" status is open if "0" it's closed
    /// </summary>
    public string EstablishmentStatusAsString => EstablishmentStatusCode == "1" ? "Open" : EstablishmentStatusCode == "0" ? "Closed" : string.Empty;
   
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