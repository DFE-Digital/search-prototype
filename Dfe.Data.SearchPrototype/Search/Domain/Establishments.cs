using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain
{
    public sealed class Establishments : AggregateRoot<EstablismentsIdentifier>
    {
        private readonly List<Establishment> _establishments = [];

        public int EstablismentCount => _establishments.Count;
        public IReadOnlyCollection<Establishment> EstablismentResults =>  _establishments.AsReadOnly();

        public Establishments(EstablismentsIdentifier establismentsIdentifier) : base(establismentsIdentifier){
        }

        public void AddEstablismentResult(Establishment establishment)
        {
            _establishments.Add(establishment);
        }

        public override void EnsureValidState()
        {
            // TODO: we need to loop through and ensure valid state!!!!!
        }
    }
}
