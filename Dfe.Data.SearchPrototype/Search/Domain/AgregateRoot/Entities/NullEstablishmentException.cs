namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities
{
    /// <summary>
    /// Targeted exception to denote null establishment errors.
    /// </summary>
    public sealed class NullEstablishmentException : Exception
    {
        /// <summary>
        /// This exception type expects a meaningful message to be passed on initialisation
        ///  which describes in detail the context of the null establishment error.
        /// </summary>
        /// <param name="message">
        /// Targeted exception message describing the details of the error.
        /// </param>
        public NullEstablishmentException(string message) : base(message){
        }
    }
}
