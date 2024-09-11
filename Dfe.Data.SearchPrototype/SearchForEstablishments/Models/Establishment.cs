namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// Encapsulates the establishment search result.
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
    public string EstablishmentType { get; }

    /// <summary>
    /// The read-only education phase of establishment.
    /// </summary>
    public string PhaseOfEducation { get; }

    /// <summary>
    /// The read-only status of the establishment.
    /// </summary>
    public string EstablishmentStatusName { get; }

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
    /// <param name="phaseOfEducation">
    /// The Phase of education of given establishment.
    /// </param>
    /// /// <param name="establishmentStatusName">
    /// The status of the given establishment.
    /// </param>
    public Establishment(string urn, string name, Address address, string establishmentType, string phaseOfEducation, string establishmentStatusName)
    {
        Urn = urn;
        Name = name;
        Address = address;
        EstablishmentType = establishmentType;
        PhaseOfEducation = phaseOfEducation;
        EstablishmentStatusName = establishmentStatusName;
    }
}
