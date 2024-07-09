using Dfe.Data.SearchPrototype.Search.Domain.Core.DfE.ComponentLibrary.DomainDrivenDesign.Domain;

namespace Dfe.Data.SearchPrototype.Search.Domain.Core
{
    /// <summary>
    /// The aggregate root defines the root entity which aggregates other entities and value objects,
    /// thus defining a boundary around the aggregation. External interactors are  nly allowed to hold
    /// references to the aggregate, which defines the attributes and invariants of the domain model as
    /// a whole, and assigns responsibility for their orchestration and composition to the aggregation root through invariants.
    /// </summary>
    /// <typeparam name="TIdentifier">
    /// Identifier which encapsulates the uniqueness of the specific aggregate instance.
    /// </typeparam>
    public abstract class AggregateRoot<TIdentifier> : Entity<TIdentifier>
        where TIdentifier : ValueObject<TIdentifier>
    {
        protected AggregateRoot(TIdentifier identifier)
            : base(identifier){
        }

        /// <summary>
        /// Provides access to the read-only identifier prescribed.
        /// </summary>
        public TIdentifier AggregateId => Identifier;
    }
}
