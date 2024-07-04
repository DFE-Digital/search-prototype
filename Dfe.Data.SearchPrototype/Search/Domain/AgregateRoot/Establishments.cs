using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities;
using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects;
using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Establishments : AggregateRoot<EstablismentsIdentifier>
    {
        private readonly List<Establishment> _establishments = [];

        /// <summary>
        /// 
        /// </summary>
        public int EstablismentCount => _establishments.Count;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyCollection<Establishment> EstablismentResults => _establishments.AsReadOnly();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="establismentsIdentifier"></param>
        public Establishments(EstablismentsIdentifier establismentsIdentifier) : base(establismentsIdentifier)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="establishment"></param>
        /// <exception cref="NullEstablishmentException"></exception>
        public void AddEstablismentResult(Establishment establishment)
        {
            if (establishment is null)
            {
                throw new NullEstablishmentException();
            }

            _establishments.Add(establishment);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void EnsureValidState()
        {
            // TODO: we need to loop through and ensure valid state!!!!!
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Establishments Create() =>
            new(new EstablismentsIdentifier(Guid.NewGuid()));
    }
}
