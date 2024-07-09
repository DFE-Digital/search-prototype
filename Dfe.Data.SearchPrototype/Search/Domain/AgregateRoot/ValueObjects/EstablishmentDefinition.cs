using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects
{
    /// <summary>
    /// Object used to encapsulate the primitives which define an establishment (i.e. name).
    /// </summary>
    public class EstablishmentDefinition : ValueObject<EstablishmentDefinition>
    {
        /// <summary>
        /// The read-only name of the establishment.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Provision of the establishment name via the constructor, together with a
        /// read-only accessor ensures immutabilit of the prescribed institution name.
        /// </summary>
        /// <param name="name">
        /// The establisment name to be assigned.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Exception thrown if an invalid name string is provided.
        /// </exception>
        public EstablishmentDefinition(string name)
        {
            Name = (string.IsNullOrWhiteSpace(name)) ?
                throw new ArgumentNullException(nameof(name)) : name;
        }

        /// <summary>
        /// Provides the means by which to expose and aggregate
        /// the object's properties for equality checking.
        /// </summary>
        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return Name;
        }
    }
}
