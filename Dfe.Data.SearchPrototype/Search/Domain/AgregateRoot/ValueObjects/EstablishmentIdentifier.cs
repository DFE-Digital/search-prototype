using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class EstablishmentIdentifier : ValueObject<EstablishmentIdentifier>
    {
        /// <summary>
        /// 
        /// </summary>
        public int EstablismentId { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="establishmentId"></param>
        public EstablishmentIdentifier(int establishmentId)
        {
            //if (establishmentId Guid.Empty)
            //    throw new ArgumentException(
            //        "The establishment result Id must have a valid GUID assigned.");

            EstablismentId = establishmentId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return EstablismentId;
        }
    }
}
