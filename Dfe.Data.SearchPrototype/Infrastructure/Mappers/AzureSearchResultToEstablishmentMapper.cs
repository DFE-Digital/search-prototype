using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.DataTransferObjects;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

/// <summary>
/// Facilitates mapping from the received T:Dfe.Data.SearchPrototype.Infrastructure.Establishment
/// into the required T:Dfe.Data.SearchPrototype.SearchForEstablishments.Establishment object.
/// </summary>
public sealed class AzureSearchResultToEstablishmentMapper : IMapper<DataTransferObjects.Establishment, SearchForEstablishments.Models.Establishment>
{
    private readonly IMapper<DataTransferObjects.Establishment, Address> _addressMapper;

    /// <summary>
    /// The following mapping dependency provides the functionality to map from a <see cref="Establishment" />
    /// object, to a configured <see cref="Address" /> instance, the complete
    /// implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="addressMapper">Address mapper instance.</param>
    public AzureSearchResultToEstablishmentMapper(
        IMapper<DataTransferObjects.Establishment, Address> addressMapper)
    {
        _addressMapper = addressMapper;
    }

    /// <summary>
    /// The following mapping dependency provides the functionality to map from a raw Azure
    /// search result, to a configured <see cref="Establishment" />
    /// instance, the complete implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="input">
    /// The raw <see cref="Establishment"/> instance used to map from.
    /// </param>
    /// <returns>
    /// The configured <see cref="Establishment"/>  instance expected.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Exception thrown if the id, name, or type of an establishment is not provided
    /// </exception>
    public SearchForEstablishments.Models.Establishment MapFrom(DataTransferObjects.Establishment input)
    {
        // TODO - only throw for really essential stuff
        ArgumentException.ThrowIfNullOrEmpty(input.id);
        ArgumentException.ThrowIfNullOrEmpty(input.ESTABLISHMENTNAME);
        ArgumentException.ThrowIfNullOrEmpty(input.TYPEOFESTABLISHMENTNAME);
        ArgumentException.ThrowIfNullOrEmpty(input.PHASEOFEDUCATION);
        ArgumentException.ThrowIfNullOrEmpty(input.ESTABLISHMENTSTATUSNAME);

        return new(
            urn: input.id,
            name: input.ESTABLISHMENTNAME,
            address: _addressMapper.MapFrom(input),
            establishmentType: input.TYPEOFESTABLISHMENTNAME,
            phaseOfEducation: input.PHASEOFEDUCATION,
            establishmentStatusName: input.ESTABLISHMENTSTATUSNAME);
    }
}
