using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class EstablishmentDefinition : ValueObject<EstablishmentDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="institution"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EstablishmentDefinition(string institution)
        {
            Name = institution ?? throw new ArgumentNullException(nameof(institution));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return Name;
        }
    }
}
