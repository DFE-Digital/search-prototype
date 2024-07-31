using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

/// <summary>
/// Facilitates mapping from the received T:Dfe.Data.SearchPrototype.Infrastructure.Establishment
/// into the required T:Dfe.Data.SearchPrototype.SearchForEstablishments.EducationPhase object.
/// </summary>
public class AzureSearchResultToEducationPhaseMapper : IMapper<Infrastructure.Establishment, EducationPhase>
{
    /// <summary>
    /// The following mapping dependency provides the functionality to map from a raw Azure
    /// search result, to a configured T:Dfe.Data.SearchPrototype.SearchForEstablishments.EducationPhase
    /// instance, the complete implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="input">
    /// The raw T:Dfe.Data.SearchPrototype.Infrastructure.Establishment used to map from.
    /// </param>
    /// <returns>
    /// The configured T:Dfe.Data.SearchPrototype.SearchForEstablishments.EducationPhase instance expected.
    /// </returns>
    public EducationPhase MapFrom(Establishment input)
    {
        if (string.IsNullOrEmpty(input.ISPRIMARY))
        {
            throw new ArgumentException(nameof(input.ISPRIMARY));
        }
        if (string.IsNullOrEmpty(input.ISSECONDARY))
        {
            throw new ArgumentException(nameof(input.ISSECONDARY));
        }
        if (string.IsNullOrEmpty(input.ISPOST16))
        {
            throw new ArgumentException(nameof(input.ISPOST16));
        }
        return new(
            isPrimary: input.ISPRIMARY,
            isSecondary: input.ISSECONDARY,
            isPost16: input.ISPOST16);

    }
}