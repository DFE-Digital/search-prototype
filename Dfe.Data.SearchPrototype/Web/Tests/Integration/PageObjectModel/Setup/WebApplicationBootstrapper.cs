﻿using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;

public abstract class WebApplicationBootstrapper : IClassFixture<WebApplicationFactory<Program>>
{
    protected HttpClient HttpClient { get; }

    /// <summary>
    /// WebApplicationFactory<Program> is injected by xUnit, which creates and calls Dispose.
    /// </summary>
    protected WebApplicationBootstrapper(WebApplicationFactory<Program> fixture)
    {
        HttpClient = fixture.CreateClient();
    }
}