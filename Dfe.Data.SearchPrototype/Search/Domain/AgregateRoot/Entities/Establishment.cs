using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects;
using Dfe.Data.SearchPrototype.Search.Domain.Core.DfE.ComponentLibrary.DomainDrivenDesign.Domain;

namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities
{
    /// <summary>
    /// The entity (defined by URN) which encapsulates the information
    /// about each unique establishment through the internally defined object graph.
    /// </summary>
    public sealed class Establishment : Entity<EstablishmentIdentifier>
    {

        public EstablishmentDefinition Definition { get; set; }

        /// <summary>
        /// Requires injection of a configured T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects.EstablismentIdentifier
        /// and T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects.EstablismentDefinition
        /// which provides contextual state to the hydrated entity.
        /// </summary>
        /// <param name="establishmentIdentifier">
        /// Identifier which encapsulates the URN used to uniquely identify an establishment.
        /// </param>
        public Establishment(
            EstablishmentIdentifier establishmentIdentifier, EstablishmentDefinition definition)
                : base(establishmentIdentifier)
        {
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        }
    }
}
