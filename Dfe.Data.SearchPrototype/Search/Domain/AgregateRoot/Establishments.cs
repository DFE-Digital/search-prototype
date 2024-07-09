using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects;
using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot
{
    /// <summary>
    /// Acts as the aggregate root for all establisment related results, enforcement of this pattern
    /// ensures consistency and defines a transactional concurrency bounday for the wider object graph
    /// (i.e. entities and value objects) that are grouped under the aggregate and treated as a conceptual whole. 
    /// </summary>
    public sealed class Establishments : AggregateRoot<EstablishmentsIdentifier>
    {
        private readonly List<Establishment> _establishments = [];

        /// <summary>
        /// <para>
        /// Provides a cound over the internal collective state of the encapsulated
        /// T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities.Establishment objects.
        /// </para>
        /// <para>
        /// The aggregate supports invariants (i.e. rules that enforce the consistency in the domain model).
        /// Whenever there is a change to an establishments entities and/or value objects, a consistent
        /// business rule must always be applied (i.e. the interactor/user is not afforded the
        /// opportunity to independently mutate establishment level information, this must always
        /// be orchestrated by the aggregate.
        /// </para>
        /// </summary>
        public int EstablismentCount => _establishments.Count;

        /// <summary>
        /// Provides read-only access to the collective state of the encapsulated
        /// T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities.Establishment objects.
        /// </summary>
        public IReadOnlyCollection<Establishment> EstablismentResults => _establishments.AsReadOnly();

        /// <summary>
        /// Requires injection of a configured T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects.EstablishmentsIdentifier
        /// which provides contextual state to the hydrated aggregate and thus affords a contexual boundary.
        /// </summary>
        /// <param name="establishmentsIdentifier">
        /// Identifier which encapsulates the GUID used to uniquely identify an establishments contextual boundary.
        /// </param>
        public Establishments(EstablishmentsIdentifier establishmentsIdentifier) : base(establishmentsIdentifier){
        }

        /// <summary>
        /// Invariant that ensures consistency when applying new
        /// T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities.Establishment objects
        /// to the internally managed read-only collection.
        /// </summary>
        /// <param name="establishment">
        /// A configured instance of T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities.Establishment
        /// </param>
        /// <exception cref="NullEstablishmentException">
        /// Exception type thrown if an attempt is made to add a null
        /// T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities.Establishment
        /// object to the internal read-only collection.
        /// </exception>
        public void AddEstablishment(Establishment establishment)
        {
            if (establishment is null)
            {
                throw new NullEstablishmentException(
                    "Only configured establisment instances can be added to the read-only collection.");
            }

            _establishments.Add(establishment);
        }

        /// <summary>
        /// Factory method allowing internal creation of
        /// T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments
        /// instances with pre-configured identifiers (GUID-based).
        /// </summary>
        /// <returns>
        /// An instance of T:Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Establishments
        /// </returns>
        public static Establishments Create() => new(new EstablishmentsIdentifier(Guid.NewGuid()));
    }
}
