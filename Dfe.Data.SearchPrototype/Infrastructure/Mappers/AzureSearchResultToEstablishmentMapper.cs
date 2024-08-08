using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

/// <summary>
/// Facilitates mapping from the received T:Dfe.Data.SearchPrototype.Infrastructure.Establishment
/// into the required T:Dfe.Data.SearchPrototype.SearchForEstablishments.Establishment object.
/// </summary>
public sealed class AzureSearchResultToEstablishmentMapper : IMapper<Establishment, SearchForEstablishments.Models.Establishment>
{
    private readonly IMapper<Establishment, Address> _addressMapper;
    private readonly IMapper<Establishment, EducationPhase> _educationPhaseMapper;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="addressMapper">Address mapper instance</param>
    /// <param name="educationPhaseMapper">EducationPhase mapper instance</param>
    public AzureSearchResultToEstablishmentMapper(
        IMapper<Establishment, Address> addressMapper,
        IMapper<Establishment, EducationPhase> educationPhaseMapper)
    {
        _addressMapper = addressMapper;
        _educationPhaseMapper = educationPhaseMapper;
    }

    /// <summary>
    /// The following mapping dependency provides the functionality to map from a raw Azure
    /// search result, to a configured T:Dfe.Data.SearchPrototype.Search.Establishment
    /// instance, the complete implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="input">
    /// The raw T:Dfe.Data.SearchPrototype.Infrastructure.Establishment used to map from.
    /// </param>
    /// <returns>
    /// The configured T:Dfe.Data.SearchPrototype.Search.Establishment instance expected.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Exception thrown if the id, name, or type of an establishment is not provided
    /// </exception>
    public SearchForEstablishments.Models.Establishment MapFrom(Establishment input)
    {
        ArgumentException.ThrowIfNullOrEmpty(input.id, nameof(input.id));
        ArgumentException.ThrowIfNullOrEmpty(input.ESTABLISHMENTNAME, nameof(input.ESTABLISHMENTNAME));
        ArgumentException.ThrowIfNullOrEmpty(input.TYPEOFESTABLISHMENTNAME, nameof(input.ESTABLISHMENTNAME));

        var statusCode = input.ESTABLISHMENTSTATUSCODE == "1"
                    ? EstablishmentStatusCode.Open
                        : input.ESTABLISHMENTSTATUSCODE == "0"
                        ? EstablishmentStatusCode.Closed
                            : EstablishmentStatusCode.Unknown;

        return new(
            urn: input.id,
            name: input.ESTABLISHMENTNAME,
            address: _addressMapper.MapFrom(input),
            establishmentType: input.TYPEOFESTABLISHMENTNAME,
            educationPhase: _educationPhaseMapper.MapFrom(input),
            establishmentStatusCode: statusCode);
    }
}
