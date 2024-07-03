using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain
{
    public class EstablismentsIdentifier : ValueObject<EstablismentsIdentifier>
    {
        public Guid EstablismentResultsId { get; }

        public EstablismentsIdentifier(Guid establishmentResultId)
        {
            if (establishmentResultId == Guid.Empty)
                throw new ArgumentException(
                    "The establishment result Id must have a valid GUID assigned.");

            EstablismentResultsId = establishmentResultId;
        }

        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return EstablismentResultsId;
        }
    }
}
