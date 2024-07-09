using Dfe.Data.SearchPrototype.Search.Domain.Core.DfE.ComponentLibrary.DomainDrivenDesign.Domain;

namespace Dfe.Data.SearchPrototype.Search.Domain.Core
{
    public abstract class AggregateRoot<TIdentifier> : Entity<TIdentifier>
        where TIdentifier : ValueObject<TIdentifier>
    {
        protected AggregateRoot(TIdentifier identifier)
            : base(identifier){
        }

        public TIdentifier AggregateId => Identifier;
    }
}
