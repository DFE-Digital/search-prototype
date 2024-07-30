using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

public class AzureSearchResultToEducationPhaseMapper : IMapper<Infrastructure.Establishment, EducationPhase>
{
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