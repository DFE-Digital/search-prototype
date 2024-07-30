namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

/// <summary>
/// Object used to encapsulate the establishment search result.
/// </summary>
public class Establishment
{
    /// <summary>
    /// The read-only URN (unique identifier) of the given establishment.
    /// </summary>
    public string Urn { get; }
    /// <summary>
    /// The read-only name associated with the given establishment.
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// The read-only address associated with the given establishment
    /// </summary>
    public Address Address { get; }
    /// <summary>
    /// The read-only type of the establishment.
    /// </summary>
    public string EstablishmentType {  get; }
    public EducationPhase EducationPhase { get; }

    /// <summary>
    /// Establishes an immutable establishment instance via the constructor arguments specified.
    /// </summary>
    /// <param name="urn">
    /// The URN (unique identifier) of the given establishment.
    /// </param>
    /// <param name="name">
    /// The name associated with the given establishment.
    /// </param>
    /// <param name="address">
    /// The address of the given establishment.
    /// </param>
    /// <param name="establishmentType">
    /// The type of the given establishment.
    /// </param>
    public Establishment(string urn, string name, Address address, string establishmentType, EducationPhase educationPhase)
    {
        Urn = urn;
        Name = name;
        Address = address;
        EstablishmentType = establishmentType;
        EducationPhase = educationPhase;
    }
}
