using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class EstablismentsIdentifier : ValueObject<EstablismentsIdentifier>
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid EstablismentResultsId { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="establishmentResultId"></param>
        /// <exception cref="ArgumentException"></exception>
        public EstablismentsIdentifier(Guid establishmentResultId)
        {
            if (establishmentResultId == Guid.Empty)
            {
                throw new ArgumentException(
                    "The establishment result Id must have a valid GUID assigned.");
            }
            EstablismentResultsId = establishmentResultId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return EstablismentResultsId;
        }
    }
}
