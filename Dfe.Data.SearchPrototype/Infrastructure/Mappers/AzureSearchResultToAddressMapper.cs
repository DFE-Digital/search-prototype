using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

/// <summary>
/// Facilitates mapping from the received <see cref="Establishment"/> instance
/// into the required <see cref="Address"/> object.
/// </summary>
public class AzureSearchResultToAddressMapper : IMapper<DataTransferObjects.Establishment, Address>
{
    /// <summary>
    /// The following mapping dependency provides the functionality to map from a raw Azure
    /// search result, to a configured <see cref="Address"/> instance, the complete
    /// implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="input">
    /// The raw <see cref="Establishment"/> instance used to map from.
    /// </param>
    /// <returns>
    /// The configured <see cref="Address"/> instance expected.
    /// </returns>
    public Address MapFrom(DataTransferObjects.Establishment input)
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
