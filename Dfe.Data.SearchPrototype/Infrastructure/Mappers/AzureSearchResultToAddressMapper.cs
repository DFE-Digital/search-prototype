using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

public class AzureSearchResultToAddressMapper : IMapper<Infrastructure.Establishment, Address>
{
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
