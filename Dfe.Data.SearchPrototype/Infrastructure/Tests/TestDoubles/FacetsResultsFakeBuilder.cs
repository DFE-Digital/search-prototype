﻿using Azure.Search.Documents.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public class FacetsResultsFakeBuilder
{
    private Dictionary<string, IList<FacetResult>> _facets = new();

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

        foreach(var kvp in facet)
        {
            _facets.Add(kvp.Key, kvp.Value);
        }
        return this;
    }

    public FacetsResultsFakeBuilder WithAutoGeneratedFacets()
    {
        var facetsCount = new Bogus.Faker().Random.Int(1, 10);
        for(int i=0; i<facetsCount;  i++)
        {
            _ = WithAutoGeneratedFacet(i.ToString());
        }
        return this;
    }

    public FacetsResultsFakeBuilder WithAutoGeneratedFacets(List<string> facetNames)
    {
        foreach (var facetName in facetNames)
        {
            _ = WithAutoGeneratedFacet(facetName);
        }
        return this;
    }

    public FacetsResultsFakeBuilder WithAutoGeneratedFacet(string facetName, string? appendFacetName = null)
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

        Dictionary<string, IList<FacetResult>> facet = new() { [facetName + appendFacetName] = facetResults };

        foreach (var kvp in facet)
        {
            _facets.Add(kvp.Key, kvp.Value);
        }
        return this;
    }

    public FacetsResultsFakeBuilder WithFacetValue(List<object> facetParams)
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
