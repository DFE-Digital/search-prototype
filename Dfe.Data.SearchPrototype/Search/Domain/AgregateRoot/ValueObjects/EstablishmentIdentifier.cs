using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects
{
    /// <summary>
    /// Object used to encapsulate the establishment identity (i.e. URN).
    /// </summary>
    public class EstablishmentIdentifier : ValueObject<EstablishmentIdentifier>
    {
        /// <summary>
        /// The read-only URN of the establishment.
        /// </summary>
        public string URN { get; }

        /// <summary>
        /// Provision of the establishment URN via the constructor, together with a
        /// read-only accessor ensures immutability of the prescribed institution identity.
        /// </summary>
        /// <param name="urn">
        /// The establisment URN to be assigned.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Exception thrown if an invalid URN string is provided.
        /// </exception>
        public EstablishmentIdentifier(string urn)
        {
            if (string.IsNullOrWhiteSpace(urn))
            {
                throw new ArgumentNullException(nameof(urn));
            }

            URN = urn;
        }

        /// <summary>
        /// Provides the means by which to expose and aggregate
        /// the object's properties for equality checking.
        /// </summary>
        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return URN;
        }
    }
}