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
        public Guid EstablismentsRootId { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="establishmentResultId"></param>
        /// <exception cref="ArgumentException"></exception>
        public EstablismentsIdentifier(Guid establismentsRootId)
        {
            if (establismentsRootId == Guid.Empty)
            {
                throw new ArgumentException(
                    "The establishment root Id must have a valid GUID assigned.");
            }
            EstablismentsRootId = establismentsRootId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return EstablismentsRootId;
        }
    }
}
