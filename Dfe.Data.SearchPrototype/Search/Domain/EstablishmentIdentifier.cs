using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain
{
    public class EstablishmentIdentifier : ValueObject<EstablishmentIdentifier>
    {
        public int EstablismentId { get; }

        public EstablishmentIdentifier(int establishmentId)
        {
            //if (establishmentId Guid.Empty)
            //    throw new ArgumentException(
            //        "The establishment result Id must have a valid GUID assigned.");

            EstablismentId = establishmentId;
        }

        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return EstablismentId;
        }
    }
}
