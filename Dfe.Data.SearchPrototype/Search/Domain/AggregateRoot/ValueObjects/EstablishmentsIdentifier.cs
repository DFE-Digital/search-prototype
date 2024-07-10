using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain.AggregateRoot.ValueObjects
{
    /// <summary>
    /// Object used to encapsulate the establishments identity (i.e. GUID).
    /// </summary>
    public class EstablishmentsIdentifier : ValueObject<EstablishmentsIdentifier>
    {
        /// <summary>
        /// The read-only root Id (GUID) of the establishment.
        /// </summary>
        public Guid EstablishmentsRootId { get; }

        /// <summary>
        /// Provision of the establishment root id (GUID) via the constructor, together with
        /// read-only access ensures immutability of the prescribed aggregate root identity.
        /// </summary>
        /// <param name="establishmentsRootId">
        /// The establishment root id (GUID) to be assigned.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Exception thrown if an invalid GUID is provided.
        /// </exception>
        public EstablishmentsIdentifier(Guid establishmentsRootId)
        {
            if (establishmentsRootId == Guid.Empty)
            {
                throw new ArgumentException(
                    "The establishment root Id must have a valid GUID assigned.");
            }
            EstablishmentsRootId = establishmentsRootId;
        }

        /// <summary>
        /// Provides the means by which to expose and aggregate
        /// the object's properties for equality checking.
        /// </summary>
        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return EstablishmentsRootId;
        }
    }
}
