﻿namespace Dfe.Data.SearchPrototype.Infrastructure.DataTransferObjects;

/// <summary>
/// Data transformation object used to encapsulate the core response from an Azure cognitive search result.
/// </summary>
public class Establishment
{
    /// <summary>
    /// The unique identifier of the retrieved establishment result.
    /// </summary>
    public string? id { get; set; }

    /// <summary>
    /// The name associated with the retrieved establishment result.
    /// </summary>
    public string? ESTABLISHMENTNAME { get; set; }

    /// <summary>
    /// First line of the address
    /// </summary>
    public string? STREET { get; set; }

    /// <summary>
    /// Second line of the address of the retrieved establishment result
    /// </summary>
    public string? LOCALITY { get; set; }

    /// <summary>
    /// Third line of the address of the retrieved establishment result
    /// </summary>
    public string? ADDRESS3 { get; set; }

    /// <summary>
    /// Fourth line of the address of the retrieved establishment result
    /// </summary>
    public string? TOWN { get; set; }

    /// <summary>
    /// Postcode of the retrieved establishment result
    /// </summary>
    public string? POSTCODE { get; set; }

    /// <summary>
    /// The type of the establishment of the retrieved establishment result
    /// </summary>
    public string? TYPEOFESTABLISHMENTNAME { get; set; }

    /// <summary>
    /// The education phase of the retrieved establishment result
    /// </summary>
    public string? PHASEOFEDUCATION { get; set; }

    /// <summary>
    /// Status of retrieved establishment result.
    /// </summary>
    public string? ESTABLISHMENTSTATUSNAME { get; set; }
}
