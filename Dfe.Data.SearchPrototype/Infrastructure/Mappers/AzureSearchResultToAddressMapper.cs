using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

/// <summary>
/// Facilitates mapping from the received T:Dfe.Data.SearchPrototype.Infrastructure.Establishment
/// into the required T:Dfe.Data.SearchPrototype.SearchForEstablishments.Address object.
/// </summary>
public class AzureSearchResultToAddressMapper : IMapper<Infrastructure.Establishment, Address>
{
    /// <summary>
    /// The following mapping dependency provides the functionality to map from a raw Azure
    /// search result, to a configured T:Dfe.Data.SearchPrototype.SearchForEstablishments.Address
    /// instance, the complete implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="input">
    /// The raw T:Dfe.Data.SearchPrototype.Infrastructure.Establishment used to map from.
    /// </param>
    /// <returns>
    /// The configured T:Dfe.Data.SearchPrototype.SearchForEstablishments.Address instance expected.
    /// </returns>
    public Address MapFrom(Establishment input)
    {
        return new()
        {
            Street = input.STREET,
            Locality = input.LOCALITY,
            Address3 = input.ADDRESS3,
            Town = input.TOWN,
            Postcode = input.POSTCODE
        };
    }
}
