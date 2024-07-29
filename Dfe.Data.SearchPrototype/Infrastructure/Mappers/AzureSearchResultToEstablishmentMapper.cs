﻿using Dfe.Data.SearchPrototype.SearchForEstablishments;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mappers;

/// <summary>
/// Facilitates mapping from the received T:Dfe.Data.SearchPrototype.Search.Establishment
/// into the required T:Dfe.Data.SearchPrototype.Search.Establishment object.
/// </summary>
public sealed class AzureSearchResultToEstablishmentMapper : IMapper<Establishment, SearchForEstablishments.Establishment>
{
    private readonly IMapper<Establishment, Address> _addressMapper;
    private readonly IMapper<Establishment, EducationPhase> _educationPhaseMapper;

    public AzureSearchResultToEstablishmentMapper(
            IMapper<Establishment, Address> addressMapper,
            IMapper<Establishment, EducationPhase> educationPhaseMapper)
    {
        _addressMapper = addressMapper;
        _educationPhaseMapper = educationPhaseMapper;
    }

    /// <summary>
    /// The following mapping dependency provides the functionality to map from a raw Azure
    /// search result, to a configured T:Dfe.Data.SearchPrototype.Search.Establishment
    /// instance, the complete implementation of which is defined in the IOC container.
    /// </summary>
    /// <param name="input">
    /// The raw T:Dfe.Data.SearchPrototype.Infrastructure.Establishment used to map from.
    /// </param>
    /// <returns>
    /// The configured T:Dfe.Data.SearchPrototype.Search.Establishment instance expected.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Exception thrown if either the id or name of an establishment is not provided
    /// </exception>
    public SearchForEstablishments.Establishment MapFrom(Establishment input)
    {
        if (string.IsNullOrEmpty(input.id)){
            throw new ArgumentException(nameof(input.id));
        }

        if (string.IsNullOrEmpty(input.ESTABLISHMENTNAME)){
            throw new ArgumentException(nameof(input.ESTABLISHMENTNAME));
        }

        return new(urn: input.id,
            name: input.ESTABLISHMENTNAME,
            address: _addressMapper.MapFrom(input),
            educationPhase:_educationPhaseMapper.MapFrom(input));
    }
}
