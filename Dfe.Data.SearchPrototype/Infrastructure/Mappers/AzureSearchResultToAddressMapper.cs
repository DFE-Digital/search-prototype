using Dfe.Data.SearchPrototype.SearchForEstablishments;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

public class AzureSearchResultToAddressMapper : IMapper<Infrastructure.Establishment, Address>
{
    public Address MapFrom(Establishment input)
    {
        throw new NotImplementedException();
    }
}
