using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects
{
    /// <summary>
    /// Object used to encapsulate the establishments identity (i.e. GUID).
    /// </summary>
    public class EstablismentsIdentifier : ValueObject<EstablismentsIdentifier>
    {
        /// <summary>
        /// The read-only root Id (GUID) of the establishment.
        /// </summary>
        public Guid EstablismentsRootId { get; }

        /// <summary>
        /// Provision of the establishment root id (GUID) via the constructor, together with a
        /// read-only accessor ensures immutability of the prescribed aggregate root identity.
        /// </summary>
        /// <param name="establismentsRootId">
        /// The establisment root id (GUID) to be assigned.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Exception thrown if an invalid GUID is provided.
        /// </exception>
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
        /// Provides the means by which to expose and aggregate
        /// the object's properties for equality checking.
        /// </summary>
        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return EstablismentsRootId;
        }
    }
}
