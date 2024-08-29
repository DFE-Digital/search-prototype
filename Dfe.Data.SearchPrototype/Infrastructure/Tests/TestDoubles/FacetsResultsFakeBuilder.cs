﻿using Azure.Search.Documents.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public class FacetsResultsFakeBuilder
{
    private Dictionary<string, IList<FacetResult>>? _facets;

    public FacetsResultsFakeBuilder WithEducationPhaseFacet()
    {
        var facetResults = new List<FacetResult>();
        int resultsCountAnyNumber = new Bogus.Faker().Random.Int(1, 50);
        var facetResult = new Dictionary<string, object>()
        {
            ["value"] = "Primary"
        };
        facetResults.Add(SearchModelFactory.FacetResult(resultsCountAnyNumber, facetResult));
        facetResult = new Dictionary<string, object>()
        {
            ["value"] = "Secondary"
        };
        facetResults.Add(SearchModelFactory.FacetResult(resultsCountAnyNumber, facetResult));
        facetResult = new Dictionary<string, object>()
        {
            ["value"] = "Post16"
        };
        facetResults.Add(SearchModelFactory.FacetResult(resultsCountAnyNumber, facetResult));
        Dictionary<string, IList<FacetResult>> facet = new() { ["EducationPhase"] = facetResults };

        if (_facets == null)
        {
            _facets = new Dictionary<string, IList<FacetResult>>();
        }
        foreach(var kvp in facet)
        {
            _facets.Add(kvp.Key, kvp.Value);
        }
        return this;
    }

    public FacetsResultsFakeBuilder WithAutoGeneratedFacet()
    {
        var facetResults = new List<FacetResult>();
        int resultsCountAnyNumber = new Bogus.Faker().Random.Int(1, 50);
        var facetValuesCount = new Bogus.Faker().Random.Int(1, 10);
        for(int i=0; i<facetValuesCount; i++)
        {
            var facetResult = new Dictionary<string, object>()
            {
                ["value"] = new Bogus.Faker().Name.JobTitle()
            };
            facetResults.Add(SearchModelFactory.FacetResult(resultsCountAnyNumber, facetResult));
        }

        Dictionary<string, IList<FacetResult>> facet = new() { [new Bogus.Faker().Name.JobType()] = facetResults };

        if (_facets == null)
        {
            _facets = new Dictionary<string, IList<FacetResult>>();
        }
        foreach (var kvp in facet)
        {
            _facets.Add(kvp.Key, kvp.Value);
        }
        return this;
    }

    public FacetsResultsFakeBuilder WithFacet(List<object> facetParams)
    {
        var facetResults = new List<FacetResult>();
        int resultsCountAnyNumber = new Bogus.Faker().Random.Int(1, 50);
        var facetValuesCount = new Bogus.Faker().Random.Int(1, 10);
        foreach (var facetParam in facetParams)
        {
            var facetResult = new Dictionary<string, object>()
            {
                ["value"] = facetParam
            };
            facetResults.Add(SearchModelFactory.FacetResult(resultsCountAnyNumber, facetResult));
        }

        Dictionary<string, IList<FacetResult>> facet = new() { [new Bogus.Faker().Name.JobType()] = facetResults };

        if (_facets == null)
        {
            _facets = new Dictionary<string, IList<FacetResult>>();
        }
        foreach (var kvp in facet)
        {
            _facets.Add(kvp.Key, kvp.Value);
        }
        return this;
    }

    public Dictionary<string, IList<FacetResult>> Create()
    {
        if (_facets == null)
        {
            throw new NullReferenceException("Facet fake has no facets set");
        }
        return _facets;
    }
}
